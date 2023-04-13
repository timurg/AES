using AES.BusinessLogic;
using AES.BusinessLogic.Implementation;
using AES.Infrastructure;
using AES.Story;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
var serviceProvider = ConfigureServices(configuration) ?? throw new ApplicationException();
if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

var botClient = new BotClient("6291851683:AAGz8AXfuHtNV1NcoAibWWuW3iuGDUumFNw");
var me = botClient.GetMe();
Console.WriteLine($"Start listening for @{me.Username}");

var updates = await botClient.GetUpdatesAsync();
Console.WriteLine("Enter to cycle");
botClient.SetMyCommands(new BotCommand("info", "Информация"), new BotCommand("/next", "Далее"));
while (true)
{
    Console.WriteLine("tic");
    if (updates.Any())
    {
        Console.WriteLine("Detect updates");
        foreach (var update in updates)
        {
            using (var unitOfWork = serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork)
            {
                try
                {
                    var userFinder = serviceProvider.GetService(typeof(IUserFinder)) as IUserFinder;
                    var user = userFinder.findByLogin(update.Message.From.Id.ToString());
                    var message = update.Message;
                    var hasText = !string.IsNullOrEmpty(message.Text);
                    if (user == null)
                    {
                        botClient.SendMessage(message.Chat.Id,
                            "Привет пользователь " + update.Message.From.Id.ToString());
                    }
                    else
                    {
                        if (hasText && message.Text == "/info")
                        {
                            var eduItem = user.Student.Curriculum.Modules.First().Items.First();
                            var isStarted = eduItem.LearningProcess != null;
                            botClient.SendMessage(message.Chat.Id,
                                $"Текущая дисциплина: {eduItem.Subject.Name}, процесс обучения начат: {isStarted}");
                        }
                        else if (hasText && (message.Text == "/next" || message.Text.ToLower()=="далее"))
                        {
                            var eduItem = user.Student.Curriculum.Modules.First().Items.First();
                            var isStarted = eduItem.LearningProcess != null;
                            MyStory story;
                            StoryItem storyItem;
                            if (!isStarted)
                            {
                                ILearningProcessBuilder builder = new MyStoryLearningProcessBuilder(unitOfWork);
                                eduItem.LearningProcess = builder.CreateLearningProcess(user.Student, eduItem);
                                eduItem.LearningProcess.BeginLearning();
                                story = eduItem.LearningProcess as MyStory;
                                storyItem = story.GetCurrentStoryItem();
                            }
                            else
                            {
                                story = eduItem.LearningProcess as MyStory;
                                storyItem = story.NextStep();
                            }

                            if (storyItem is StoryImage storyItemImage)
                            {
                                var file = new InputFile(storyItemImage.Template.Bits, storyItemImage.Template.FileName);
                                var buttons = new KeyboardButton[]
                                {
                                    new KeyboardButton("Далее"), //col 2 row 1
                                };
                                var keyboard = new ReplyKeyboardMarkup
                                {
                                    Keyboard = new []{buttons},
                                    ResizeKeyboard = true
                                }; 
                                
                                botClient.SendPhoto(message.Chat.Id, file, caption:$"Шаг {story.StoryStep+1} из {story.StoryTemplate.Items.Count}", replyMarkup:keyboard);
                            }
                            else if (storyItem == null)
                            {
                                botClient.SendMessage(message.Chat.Id, $"Обучение завершено.");
                            }
                            else
                            {
                                botClient.SendMessage(message.Chat.Id, $"Текущий шаг: {storyItem.Id}");
                            }

                        }
                        else
                        {
                            botClient.SendMessage(message.Chat.Id, $"Привет {user.Name}, ты написал:\n" + message.Text);
                        }

                    }
                    unitOfWork.Commit();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
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