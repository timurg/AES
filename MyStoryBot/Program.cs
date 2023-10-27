using System.Net.Mime;
using System.Text;
using AES.BusinessLogic;
using AES.BusinessLogic.Implementation;
using AES.Domain;
using AES.Infrastructure;
using AES.Story;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyStoryBot;
using MyStoryBot.Commands;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableMethods.FormattingOptions;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using Telegram.BotAPI.UpdatingMessages;
using Module = AES.Domain.Module;
NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddUserSecrets<Program>()
    .Build();
var serviceProvider = ConfigureServices(configuration) as AutofacServiceProvider ?? throw new ApplicationException();
if (serviceProvider == null) throw new ArgumentNullException(paramName: "serviceProvider");

var botClient = new BotClient(configuration["bot:id"]);

var me = botClient.GetMe();
_logger.Debug($"Start listening for @{me.Username}");

var updates = await botClient.GetUpdatesAsync();
_logger.Debug("Enter to cycle");
botClient.SetMyCommands(new BotCommand("info", "Информация"), new BotCommand("/next", "Далее"));
var unitOfWorkFactory = serviceProvider.GetService(typeof(IUnitOfWorkFactory)) as IUnitOfWorkFactory;

ICollection<NamedCommand> namedCommands = new List<NamedCommand>();
namedCommands.Add(new InfoCommand(botClient));
namedCommands.Add(new NextCommand(botClient));
namedCommands.Add(new AllResultsCommand(botClient));

namedCommands.Add(new ShowCommand(botClient));
namedCommands.Add(new StartLearningCommand(botClient));

while (true)
{
    _logger.Trace("tic");
    if (updates.Any())
    {
        _logger.Debug("Detect updates");
        foreach (var update in updates)
        {
            using var unitOfWork = unitOfWorkFactory.Create();
            try
            {
                CommandContext commandContext = new CommandContext();
                commandContext.UnitOfWork = unitOfWork;
                var userFinder = new UserFinder(unitOfWork); //serviceProvider.GetService(typeof(IUserFinder)) as IUserFinder;
                IEducationProcessStep processStep = new MyStoryEducationProcessStep(unitOfWork);

                User telegramUser = null;

                if (update.Message != null)
                {
                    telegramUser = update.Message.From;

                    commandContext.FromId = update.Message.From.Id;
                    commandContext.ChatId =update.Message.Chat.Id;
                    
                }
                else if (update.CallbackQuery != null)
                {
                    commandContext.FromId = update.CallbackQuery.From.Id;
                    commandContext.ChatId = update.CallbackQuery.Message.Chat.Id;
                }
                else if (update.PollAnswer != null)
                {
                    commandContext.FromId = update.PollAnswer.User.Id;
                }

                commandContext.User = userFinder.findByLogin(commandContext.FromId.ToString());

                if (commandContext.User == null)
                {
                    commandContext.User = UserUtils.InitNewUser(unitOfWork, commandContext.FromId.Value, telegramUser);
                    _logger.Info("New user created: " + commandContext.User.Login);
                    //if (chatId.HasValue)
                    //{
                    //    botClient.SendMessage(chatId.Value,
                    //        "Привет пользователь " + chatId);
                    //}
                }
                    
                {
                    var message = update.Message;
                    if (message != null)
                    {
                        commandContext.MessageId = message.MessageId;
                        commandContext.Message = message.Text;
                        var hasText = !string.IsNullOrEmpty(commandContext.Message);
                        if (hasText)
                        {
                            if (commandContext.Message.Equals("Далее", StringComparison.OrdinalIgnoreCase))
                            {
                                commandContext.Message = "/next";
                            }
                            if (commandContext.Message.StartsWith('/'))
                            {
                                var commandParts = commandContext.Message.Split(' ');
                                if (commandParts.Any())
                                {
                                    var command = commandParts[0].Trim().ToLower().Remove(0, 1);
                                    commandContext.Parameters = commandParts.Skip(1).ToArray();
                                    namedCommands.ExecuteCommand(command, commandContext);
                                }
                            }
                        }
                    }
                    else if (update.PollAnswer != null)
                    {
                        var poolId = update.PollAnswer.PollId;
                        var person = unitOfWork.PersonRepository.GetQuery().FirstOrDefault(p =>
                            p.Student.Curriculum.Modules.Any(a =>
                                a.Items.Any(b =>
                                    ((MyStory)b.LearningProcess).Items.Any(item =>
                                        ((StoryPoll)item).ObjectId == poolId))));
                        var storyPollItem = person.Student.Curriculum.Modules
                            .SelectMany(m => m.Items)
                            .Where(m => m.LearningProcess is MyStory)
                            .SelectMany(m => ((MyStory)m.LearningProcess).Items).Where(i => i is StoryPoll)
                            .First(i => ((StoryPoll)i).ObjectId == poolId) as StoryPoll;

                        var moduleItem = person.Student.Curriculum.Modules
                            .SelectMany(m => m.Items)
                            .Where(m => m.LearningProcess is MyStory)
                            .First(i => ((MyStory)i.LearningProcess).Items.Any(i => i.Id == storyPollItem.Id));

                        storyPollItem.SelectedItem = update.PollAnswer.OptionIds.First();
                        storyPollItem.CheckAnswer();

                        ProcessLearning(botClient, storyPollItem.ChatId.Value, processStep, commandContext.User.Student,
                            moduleItem);
                    }
                    else if (update.CallbackQuery != null)
                    {
                        var data = update.CallbackQuery.Data;
                        if (!string.IsNullOrWhiteSpace(data))
                        {
                            var commandData = data.Split(' ');
                            var callBackParams = new List<string>(commandData.Skip(1).ToArray());
                            callBackParams.Insert(0, update.CallbackQuery.Id);
                            commandContext.Parameters = callBackParams.ToArray();
                            
                            namedCommands.ExecuteCommand(commandData[0].Trim().ToLower(), commandContext);
                        }
                            
                    }
                }
            }
            catch (NextStepException exception)
            {
                //botClient.SendMessage(ch,
                //    $"Привет {user.Name}, ты написал:\n" + message.Text);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.ToString());
            }
                
            unitOfWork.Commit();
        }

        var offset = updates.Last().UpdateId + 1;
        updates = botClient.GetUpdates(offset);
    }
    else
    {
        updates = botClient.GetUpdates();
    }
}

static IServiceProvider ConfigureServices(IConfigurationRoot configuration)
{
    var containerBuilder = new ContainerBuilder();
    containerBuilder.RegisterModule(new ConfigurationModule(configuration.GetSection("unitOfWorkFactory")));
    containerBuilder.Register(c => c.Resolve<IUnitOfWorkFactory>().Create()).SingleInstance();
    return new AutofacServiceProvider(containerBuilder.Build());
}

static void ProcessLearning(BotClient botClient, long chatId, IEducationProcessStep processStep, Student student,
    ModuleItem eduItem)
{
    var info = processStep.Process(student, eduItem);
    if (info == ProcessState.AutoEnding)
    {
        var testItems = (eduItem.LearningProcess as MyStory).GetCurrentGenerationItems().Where(i => i is StoryPoll);
        if (testItems.Any())
        {
            botClient.SendMessage(chatId,
                $"Курс завершён. Задано тестовых заданий/ Получен правильный ответ : {testItems.Count()}/{testItems.Count(i => ((StoryPoll)i).IsPassed.Value)}.", disableNotification: true);
        }
        else
        {
            botClient.SendMessage(chatId,
                $"Курс завершён.", disableNotification: true);
        }
    }
}
