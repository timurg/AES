﻿using System.Net.Mime;
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
if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

var botClient = new BotClient(configuration["bot:id"]);

var me = botClient.GetMe();
_logger.Debug($"Start listening for @{me.Username}");

var updates = await botClient.GetUpdatesAsync();
_logger.Debug("Enter to cycle");
botClient.SetMyCommands(new BotCommand("info", "Информация"), new BotCommand("/next", "Далее"));
var unitOfWorkFactory = serviceProvider.GetService(typeof(IUnitOfWorkFactory)) as IUnitOfWorkFactory;
while (true)
{
    _logger.Trace("tic");
    if (updates.Any())
    {
        _logger.Debug("Detect updates");
        foreach (var update in updates)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                try
                {
                    var userFinder = new UserFinder(unitOfWork); //serviceProvider.GetService(typeof(IUserFinder)) as IUserFinder;
                    IEducationProcessStep processStep = new MyStoryEducationProcessStep(unitOfWork);

                    long? fromId = null;
                    long? chatId = null;
                    User telegramUser = null;

                    if (update.Message != null)
                    {
                        fromId = update.Message.From.Id;
                        chatId = update.Message.Chat.Id;
                        telegramUser = update.Message.From;
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
                        user = UserUtils.InitNewUser(unitOfWork, fromId.Value, telegramUser);
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
                            var hasText = !string.IsNullOrEmpty(message.Text);
                            if (hasText && message.Text == "/info")
                            {
                                var anyStarted = user.Student.Curriculum.Modules.First().Items.Any(i =>
                                    (i.LearningProcess != null) &&
                                    (i.LearningProcess.IsStarted()));
                                
                                foreach (var moduleItem in user.Student.Curriculum.Modules.First().Items)
                                {
                                    var isStarted = (moduleItem.LearningProcess != null) &&
                                                    (moduleItem.LearningProcess.IsStarted());
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
                                    btn[0].CallbackData = "start " + moduleItem.Id;
                                    var textMessage = new StringBuilder();
                                    if (isStarted)
                                    {
                                        textMessage.Append("Текущая дисциплина: ");
                                    }
                                    textMessage.Append(
                                        $"<b>{moduleItem.Subject.Name}</b>, процесс обучения {beginInfo}");
                                    if (moduleItem.Grade != null)
                                    {
                                        textMessage.AppendLine($"\nТекущая оценка {moduleItem.Grade.Description} ({moduleItem.Grade.GradeDateTime.DateTime.ToShortDateString()})");
                                    }
                                    //if (isStarted)
                                    //{
                                    //     textMessage.Append(eduItem.LearningProcess.CanEnd());
                                    // }
                                    if (anyStarted)
                                    {
                                        botClient.SendMessage(message.Chat.Id, textMessage.ToString(),
                                            parseMode: ParseMode.HTML, disableNotification: true);
                                    }
                                    else
                                    {
                                        botClient.SendMessage(message.Chat.Id, textMessage.ToString(),
                                            parseMode: ParseMode.HTML, replyMarkup: rm, disableNotification: true);
                                    }
                                }
                                
                                
                            }
                           /* else if (hasText && (message.Text == "/lan" || message.Text.ToLower() == "далее"))
                            {
                                botClient.SendVenue(message.Chat.Id, 54.724463f, 56.014890f,
                                    "Архитектурно-Строительный Институт, специальности Горно-Нефтяного Факультета (ГФ, ГЛ, БГЛ)",
                                    "ул. Менделеева, 195, каб. 245");
                            }*/
                            else if (hasText && (message.Text == "/next" || message.Text.ToLower() == "далее"))
                            {
                                var eduItem = user.Student.Curriculum.Modules.First().Items.FirstOrDefault(i =>
                                    (i.LearningProcess != null) &&
                                    (i.LearningProcess.IsStarted()));
                                if (eduItem != null)
                                {
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
                                    ProcessLearning(botClient, chatId.Value, processStep, user.Student, eduItem);
                                    DeleteUserCommand(botClient, message);
                                }
                                else
                                {
                                    DeleteUserCommand(botClient, message);
                                    botClient.SendMessage(message.Chat.Id,
                                        $"Наберите команду /info");
                                }
                            }
                            else if (hasText && message.Text == "/allresults")
                            {
                                if (user.Roles.Any(r => r.Name == "admin"))
                                {
                                    foreach (var student in unitOfWork.StudentRepository.GetQuery().ToArray())
                                    {
                                        var btn = new InlineKeyboardButton[1];
                                        btn[0] = new InlineKeyboardButton();
                                        var rm = new InlineKeyboardMarkup
                                        {
                                            InlineKeyboard = new[]
                                            {
                                                btn
                                            }
                                        };
                                        btn[0].Text = "Показать";
                                        btn[0].CallbackData = "show " + student.Id;
                                        botClient.SendMessage(message.Chat.Id, $"{student.Person.Login}: {student.Person.FullName}",
                                            parseMode: ParseMode.HTML, replyMarkup: rm, disableNotification: true);
                                    }
                                }
                                else
                                {
                                    botClient.SendMessage(message.Chat.Id,
                                        $"Недостаточно прав для выполнения команды.");
                                    
                                }
                            }
                            else
                            {
                                botClient.SendMessage(message.Chat.Id,
                                    $"Привет {user.Name}, набери команду /info", disableNotification: true);
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

                            ProcessLearning(botClient, storyPollItem.ChatId.Value, processStep, user.Student,
                                moduleItem);
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
                                    var isStarted = eduItem.LearningProcess != null &&
                                                    eduItem.LearningProcess.IsStarted();
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
                                        var storyItem = story.GetCurrentStoryItem();
                                        
                                        RenderItemStory(botClient, unitOfWork, chatId.Value, story, storyItem);
                                        ProcessLearning(botClient, chatId.Value, processStep, user.Student, eduItem);
                                    }
                                    botClient.AnswerCallbackQuery(new AnswerCallbackQueryArgs(update.CallbackQuery.Id)
                                    {
                                        Text = "Используйте кнопку \"Далее\" рядом с клавиатурой, для навигации.",//update.CallbackQuery.Data,
                                        ShowAlert = true
                                    });
                                }
                                else if (commandData[0] == "show")
                                {
                                    var person = unitOfWork.PersonRepository.GetQuery().First(s => s.Student.Id == id);
                                    var items
                                        = person.Student.Curriculum.Modules.First().Items;
                                    var cid = chatId.Value;
                                    var textMessage = new StringBuilder();
                                    textMessage.Append(person.FullName + "\n");
                                    int inx = 1;
                                    foreach (var moduleItem in items)
                                    {
                                        var isStarted = (moduleItem.LearningProcess != null) &&
                                                        (moduleItem.LearningProcess.IsStarted());
                                        var beginInfo = isStarted ? "стартовал\n" : "не стартовал";

                                        
                                        textMessage.Append(
                                            $"\n{inx++}) <b>{moduleItem.Subject.Name}</b>, процесс обучения {beginInfo}");
                                        if (moduleItem.Grade != null)
                                        {
                                            textMessage.AppendLine($"\nТекущая оценка {moduleItem.Grade.Description} ({moduleItem.Grade.GradeDateTime.DateTime.ToShortDateString()})");
                                        }
                                        else
                                        {
                                            textMessage.AppendLine();
                                        }
                                        
                                    }
                                    botClient.SendMessage(cid, textMessage.ToString(),
                                        parseMode: ParseMode.HTML, disableNotification: true);
                                    botClient.AnswerCallbackQuery(new AnswerCallbackQueryArgs(update.CallbackQuery.Id));
                                }
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
                    replyMarkup: keyboard, protectContent: true, disableNotification: true);
                templateWithFile.TelegramFileId = message.Photo.First().FileId;
            }
            else if (storyFileBasedItem is StoryVideo)
            {
                message = botClient.SendVideo(chatId, file,
                    caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                    replyMarkup: keyboard, protectContent: true, disableNotification: true);
                templateWithFile.TelegramFileId = message.Video.FileId;
            }
        }
        else
        {
            if (storyFileBasedItem is StoryImage)
            {
                message = botClient.SendPhoto(chatId, photo: templateWithFile.TelegramFileId,
                    caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                    replyMarkup: keyboard, protectContent: true, disableNotification: true);
            }
            else if (storyFileBasedItem is StoryVideo)
            {
                message = botClient.SendVideo(chatId, video: templateWithFile.TelegramFileId,
                    caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                    replyMarkup: keyboard, protectContent: true, disableNotification: true);
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
            ProtectContent = true
        };
        var poolMessage = botClient.SendPoll(arg);
        storyItemPool.ChatId = poolMessage.Chat.Id;
        storyItemPool.TelegramId = poolMessage.MessageId;
        storyItemPool.ObjectId = poolMessage.Poll.Id;
    }
    else if (storyItem is StoryVenue storyVenue)
    {
        var sendVenue = botClient.SendVenue(chatId, latitude: storyVenue.Latitude, longitude: storyVenue.Longitude,
            title: storyVenue.Title, address: storyVenue.Adress, disableNotification: true);
        storyVenue.ChatId = sendVenue.Chat.Id;
        storyVenue.TelegramId = sendVenue.MessageId;
        storyVenue.IsPassed = true;
    }
    else if (storyItem is StoryHtml storyHtml)
    {
        var sendMessage = botClient.SendMessage(chatId, storyHtml.Content, protectContent: true, disableNotification: true, parseMode: ParseMode.HTML);
        storyHtml.ChatId = sendMessage.Chat.Id;
        storyHtml.TelegramId = sendMessage.MessageId;
        storyHtml.IsPassed = true;
    }
    else if (storyItem == null)
    {
        botClient.SendMessage(chatId, $"Обучение завершено.", disableNotification: true);
    }
    else
    {
        botClient.SendMessage(chatId, $"Текущий шаг: {storyItem.Id}");
    }

    //DeleteUserCommand(botClient, message);
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
