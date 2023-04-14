using AES.BusinessLogic;
using AES.BusinessLogic.Implementation;
using AES.Infrastructure;
using AES.Story;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
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

                    long? fromId = null;
                    long? chatId = null;
                    
                    if (update.Message != null)
                    {
                        fromId = update.Message.From.Id;
                        chatId = update.Message.Chat.Id;
                    } else if (update.PollAnswer != null)
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
                                var isStarted = eduItem.LearningProcess != null;
                                botClient.SendMessage(message.Chat.Id,
                                    $"Текущая дисциплина: {eduItem.Subject.Name}, процесс обучения начат: {isStarted}");
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

                                if (storyItem is StoryImage storyItemImage)
                                {
                                    
                                    var buttons = new KeyboardButton[]
                                    {
                                        new KeyboardButton("Далее"), //col 2 row 1
                                    };
                                    var keyboard = new ReplyKeyboardMarkup
                                    {
                                        Keyboard = new[] { buttons },
                                        ResizeKeyboard = true
                                    };
                                    Message photoMessage;
                                    var templateImage =
                                        storyItemImage.Template as MyStoryTemplateImage;
                                    if (string.IsNullOrEmpty(templateImage.TelegramFileId))
                                    {
                                        var file = new InputFile(templateImage.Bits,
                                            templateImage.FileName);
                                        photoMessage = botClient.SendPhoto(message.Chat.Id, file,
                                            caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                                            replyMarkup: keyboard);
                                        templateImage.TelegramFileId = photoMessage.Photo.First().FileId;
                                    }
                                    else
                                    {
                                        photoMessage = botClient.SendPhoto(message.Chat.Id, photo:templateImage.TelegramFileId,
                                            caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                                            replyMarkup: keyboard);
                                    }
                                    storyItemImage.ChatId = photoMessage.Chat.Id;
                                    storyItemImage.TelegramId = photoMessage.MessageId;
                                    storyItemImage.IsPassed = true;
                                }
                                else if (storyItem is StoryPoll storyItemPool)
                                {
                                    var orderedPoolItems = storyItemPool.Items.ToArray().OrderBy(i => i.Order);
                                        
                                    var arg = new SendPollArgs(chatId: update.Message.Chat.Id,
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
                                    botClient.SendMessage(message.Chat.Id, $"Обучение завершено.");
                                }
                                else
                                {
                                    botClient.SendMessage(message.Chat.Id, $"Текущий шаг: {storyItem.Id}");
                                }

                                DeleteUserCommand(botClient, message);
                            }
                            else
                            {
                                botClient.SendMessage(message.Chat.Id,
                                    $"Привет {user.Name}, ты написал:\n" + message.Text);
                            }
                        } else if (update.PollAnswer != null)
                        {
                            var poolId = update.PollAnswer.PollId;
                            var person = unitOfWork.PersonRepository.GetQuery().FirstOrDefault(p => p.Student.Curriculum.Modules.Any(a =>
                                a.Items.Any(b => ((MyStory)b.LearningProcess).Items.Any(item => ((StoryPoll)item).ObjectId == poolId))));
                            var storyPollItem = person.Student.Curriculum.Modules
                                .SelectMany(m => m.Items)
                                .Where(m => m.LearningProcess is MyStory)
                                .SelectMany(m => ((MyStory)m.LearningProcess).Items).Where(i => i is StoryPoll).First(i => ((StoryPoll)i).ObjectId == poolId) as StoryPoll;
                            storyPollItem.SelectedItem = update.PollAnswer.OptionIds.First();
                            storyPollItem.CheckAnswer();
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