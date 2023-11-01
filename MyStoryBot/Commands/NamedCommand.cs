using AES.BusinessLogic;
using AES.Domain;
using AES.Story;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableMethods.FormattingOptions;
using Telegram.BotAPI.AvailableTypes;

namespace MyStoryBot.Commands;

public abstract class NamedCommand : BaseCommand
{
    public string CommandName { get; }

    public NamedCommand(BotClient botClient, string commandName) : base(botClient)
    {
        CommandName = commandName;
    }

    public abstract void ExecutionContext(CommandContext context);

    protected void RenderItemStory(CommandContext commandContext, MyStory story, StoryItem storyItem)
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
                var binaryData = commandContext.UnitOfWork.BinaryDataRepository.Get(templateWithFile.Id);
                var file = new InputFile(binaryData.Bits,
                    templateWithFile.FileName);
                if (storyFileBasedItem is StoryImage)
                {
                    message = _botClient.SendPhoto(commandContext.ChatId.Value, file,
                        caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                        replyMarkup: keyboard, protectContent: true, disableNotification: true);
                    templateWithFile.TelegramFileId = message.Photo.First().FileId;
                }
                else if (storyFileBasedItem is StoryVideo)
                {
                    message = _botClient.SendVideo(commandContext.ChatId.Value, file,
                        caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                        replyMarkup: keyboard, protectContent: true, disableNotification: true);
                    templateWithFile.TelegramFileId = message.Video.FileId;
                }
            }
            else
            {
                if (storyFileBasedItem is StoryImage)
                {
                    message = _botClient.SendPhoto(commandContext.ChatId.Value, photo: templateWithFile.TelegramFileId,
                        caption: $"Шаг {story.StoryStep + 1} из {story.StoryTemplate.Items.Count}",
                        replyMarkup: keyboard, protectContent: true, disableNotification: true);
                }
                else if (storyFileBasedItem is StoryVideo)
                {
                    message = _botClient.SendVideo(commandContext.ChatId.Value, video: templateWithFile.TelegramFileId,
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

            var arg = new SendPollArgs(chatId: commandContext.ChatId.Value,
                question: storyItemPool.Content,
                options: orderedPoolItems.Select(p => p.Content))
            {
                Type = "quiz",
                CorrectOptionId = orderedPoolItems.First(p => p.IsCorrect).Order,
                IsAnonymous = false,
                ProtectContent = true
            };
            var poolMessage = _botClient.SendPoll(arg);
            storyItemPool.ChatId = poolMessage.Chat.Id;
            storyItemPool.TelegramId = poolMessage.MessageId;
            storyItemPool.ObjectId = poolMessage.Poll.Id;
        }
        else if (storyItem is StoryVenue storyVenue)
        {
            var sendVenue = _botClient.SendVenue(commandContext.ChatId.Value, latitude: storyVenue.Latitude,
                longitude: storyVenue.Longitude,
                title: storyVenue.Title, address: storyVenue.Adress, disableNotification: true);
            storyVenue.ChatId = sendVenue.Chat.Id;
            storyVenue.TelegramId = sendVenue.MessageId;
            storyVenue.IsPassed = true;
        }
        else if (storyItem is StoryHtml storyHtml)
        {
            var sendMessage = _botClient.SendMessage(commandContext.ChatId.Value, storyHtml.Content,
                protectContent: true, disableNotification: true, parseMode: ParseMode.HTML);
            storyHtml.ChatId = sendMessage.Chat.Id;
            storyHtml.TelegramId = sendMessage.MessageId;
            storyHtml.IsPassed = true;
        }
        else if (storyItem == null)
        {
            _botClient.SendMessage(commandContext.ChatId.Value, $"Обучение завершено.", disableNotification: true);
        }
        else
        {
            _botClient.SendMessage(commandContext.ChatId.Value, $"Текущий шаг: {storyItem.Id}");
        }

        if (storyItem != null)
        {
            storyItem.DateViewed = DateTimeOffset.Now;
        }
        //DeleteUserCommand(botClient, message);
    }

    protected void ProcessLearning(CommandContext commandContext, IEducationProcessStep processStep, Student student,
        ModuleItem eduItem)
    {
        var info = processStep.Process(student, eduItem);
        if (info == ProcessState.AutoEnding)
        {
            var testItems = (eduItem.LearningProcess as MyStory).GetCurrentGenerationItems().Where(i => i is StoryPoll);
            if (testItems.Any())
            {
                _botClient.SendMessage(commandContext.ChatId.Value,
                    $"Курс завершён. Задано тестовых заданий/ Получен правильный ответ : {testItems.Count()}/{testItems.Count(i => ((StoryPoll)i).IsPassed.Value)}.",
                    disableNotification: true);
            }
            else
            {
                _botClient.SendMessage(commandContext.ChatId.Value,
                    $"Курс завершён.", disableNotification: true);
            }
        }
    }
}