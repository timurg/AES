using System.Text;
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
using Telegram.BotAPI.AvailableMethods.FormattingOptions;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using Telegram.BotAPI.UpdatingMessages;

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
                    IEducationProcessStep processStep = new MyStoryEducationProcessStep(unitOfWork);

                    long? fromId = null;
                    long? chatId = null;

                    if (update.Message != null)
                    {
                        fromId = update.Message.From.Id;
                        chatId = update.Message.Chat.Id;
                    }
                    else if (update.CallbackQuery != null)
                    {
                        fromId = update.CallbackQuery.From.Id;
                        chatId = update.CallbackQuery.Message.Chat.Id;
                    }
                    else if (update.PollAnswer != null)
                    {
                        fromId = update.PollAnswer.User.Id;
                    }

                    var user = userFinder.findByLogin(fromId.ToString());

                    if (user == null)
                    {
                        if (chatId.HasValue)
                        {
                            botClient.SendMessage(chatId.Value,
                                "Привет пользователь " + chatId);
                        }
                    }
                    else
                    {
                        var message = update.Message;
                        if (message != null)
                        {
                            var hasText = !string.IsNullOrEmpty(message.Text);
                            if (hasText && message.Text == "/info")
                            {
                                var eduItem = user.Student.Curriculum.Modules.First().Items.First();
                                var isStarted = (eduItem.LearningProcess != null) && (eduItem.LearningProcess.IsStarted());
                                var beginInfo = isStarted ? "стартовал" : "не стартовал";
                                var btn = new InlineKeyboardButton[1];
                                btn[0] = new InlineKeyboardButton();
                                var rm = new InlineKeyboardMarkup
                                {
                                    InlineKeyboard = new[]
                                    {
                                        btn
                                    }
                                };
                                btn[0].Text = "Начать";
                                btn[0].CallbackData = "start " + eduItem.Id;
                                var textMessage = new StringBuilder();
                                textMessage.Append(
                                    $"Текущая дисциплина: <b>{eduItem.Subject.Name}</b>, процесс обучения {beginInfo}");
                                if (isStarted)
                                {
                                    textMessage.Append(eduItem.LearningProcess.CanEnd());
                                }
                                botClient.SendMessage(message.Chat.Id,textMessage.ToString(),
                                    parseMode: ParseMode.HTML, replyMarkup: rm);
                            }
                            else if (hasText && (message.Text == "/next" || message.Text.ToLower() == "далее"))
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

                                RenderItemStory(botClient, unitOfWork, chatId.Value, story, storyItem);
                                processStep.Process(user.Student, eduItem);
                                DeleteUserCommand(botClient, message);
                            }
                            else
                            {
                                botClient.SendMessage(message.Chat.Id,
                                    $"Привет {user.Name}, ты написал:\n" + message.Text);
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
                            processStep.Process(user.Student, moduleItem);
                        }
                        else if (update.CallbackQuery != null)
                        {
                            var data = update.CallbackQuery.Data;
                            if (!string.IsNullOrWhiteSpace(data))
                            {
                                var commandData = data.Split(' ');
                                var id = new Guid(commandData[1]);
                                if (commandData[0] == "start")
                                {
                                    var eduItem = user.Student.Curriculum.Modules.SelectMany(s => s.Items)
                                        .First(s => s.Id == id);
                                    var isStarted = eduItem.LearningProcess != null && eduItem.LearningProcess.IsStarted();
                                    StoryItem storyItem;
                                    if (!isStarted)
                                    {
                                        ILearningProcessBuilder builder = new MyStoryLearningProcessBuilder(unitOfWork);
                                        if (eduItem.LearningProcess == null)
                                        {
                                            eduItem.LearningProcess =
                                                builder.CreateLearningProcess(user.Student, eduItem);
                                        }

                                        eduItem.LearningProcess.BeginLearning();
                                        var story = eduItem.LearningProcess as MyStory;
                                        storyItem = story.GetCurrentStoryItem();
                                        RenderItemStory(botClient, unitOfWork, chatId.Value, story, storyItem);
                                        processStep.Process(user.Student, eduItem);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }

                unitOfWork.Commit();
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

static void DeleteUserCommand(BotClient bot, Message message)
{
    bot.DeleteMessage(message.Chat.Id, message.MessageId);
}

static void RenderItemStory(BotClient botClient, IUnitOfWork unitOfWork, long chatId, MyStory story, StoryItem storyItem)
{
    if (storyItem is StoryFileBasedItem storyFileBasedItem)
    {
        var buttons = new KeyboardButton[]
        {
            new("Далее"),
        };
        var keyboard = new ReplyKeyboardMarkup
        {
            Keyboard = new[] { buttons },
            ResizeKeyboard = true
        };
        Message message = null;
        var templateWithFile =
            storyFileBasedItem.Template as MyStoryTemplateFileBased;
        if (string.IsNullOrEmpty(templateWithFile.TelegramFileId))
        {
            var binaryData = unitOfWork.BinaryDataRepository.Get(templateWithFile.Id);
            var file = new InputFile(binaryData.Bits,
                templateWithFile.FileName);
            if (storyFileBasedItem is StoryImage)
            {
                message = botClient.SendPhoto(chatId, file,
                    caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                    replyMarkup: keyboard);
                templateWithFile.TelegramFileId = message.Photo.First().FileId;
            }
            else if (storyFileBasedItem is StoryVideo)
            {
                message = botClient.SendVideo(chatId, file,
                    caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                    replyMarkup: keyboard);
                templateWithFile.TelegramFileId = message.Video.FileId;
            }
        }
        else
        {
            if (storyFileBasedItem is StoryImage)
            {
                message = botClient.SendPhoto(chatId, photo: templateWithFile.TelegramFileId,
                    caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                    replyMarkup: keyboard);
            }
            else if (storyFileBasedItem is StoryVideo)
            {
                message = botClient.SendVideo(chatId, video: templateWithFile.TelegramFileId,
                    caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                    replyMarkup: keyboard);
            }
        }

        if (message != null)
        {
            storyFileBasedItem.ChatId = message.Chat.Id;
            storyFileBasedItem.TelegramId = message.MessageId;
            storyFileBasedItem.IsPassed = true;
        }
    }
    else if (storyItem is StoryPoll storyItemPool)
    {
        var orderedPoolItems = storyItemPool.Items.ToArray().OrderBy(i => i.Order);

        var arg = new SendPollArgs(chatId: chatId,
            question: storyItemPool.Content,
            options: orderedPoolItems.Select(p => p.Content))
        {
            Type = "quiz",
            CorrectOptionId = orderedPoolItems.First(p => p.IsCorrect).Order,
            IsAnonymous = false,
        };
        var poolMessage = botClient.SendPoll(arg);
        storyItemPool.ChatId = poolMessage.Chat.Id;
        storyItemPool.TelegramId = poolMessage.MessageId;
        storyItemPool.ObjectId = poolMessage.Poll.Id;
    }
    else if (storyItem == null)
    {
        botClient.SendMessage(chatId, $"Обучение завершено.");
    }
    else
    {
        botClient.SendMessage(chatId, $"Текущий шаг: {storyItem.Id}");
    }

    //DeleteUserCommand(botClient, message);
}