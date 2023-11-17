using System.Text;
using AES.Domain;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableMethods.FormattingOptions;
using Telegram.BotAPI.AvailableTypes;

namespace MyStoryBot.Commands;

public class InfoCommand : NamedCommand
{
    public InfoCommand(BotClient botClient) : base(botClient, "info")
    {
    }

    public override void ExecutionContext(CommandContext context)
    {
        var anyStarted = context.User.Student.Curriculum.Modules.First().Items.Any(i =>
            (i.LearningProcess != null) &&
            (i.LearningProcess.IsStarted()));

        foreach (var moduleItem in context.User.Student.Curriculum.Modules.First().Items)
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
            btn[0].CallbackData = "startlearning " + moduleItem.Id;
            var textMessage = new StringBuilder();
            if (isStarted)
            {
                textMessage.Append("Текущая дисциплина: ");
            }

            textMessage.Append(
                $"<b>{moduleItem.Subject.Name}</b>, процесс обучения {beginInfo}");
            if (moduleItem.Grade != null)
            {
                textMessage.AppendLine(
                    $"\nТекущая оценка {moduleItem.Grade.Description} ({moduleItem.Grade.GradeDateTime.DateTime.ToShortDateString()})");
            }

            //if (isStarted)
            //{
            //     textMessage.Append(eduItem.LearningProcess.CanEnd());
            // }
            if (anyStarted)
            {
                if (context.ChatId != null)
                    BotClient.SendMessage(context.ChatId.Value, textMessage.ToString(),
                        parseMode: ParseMode.HTML, disableNotification: true);
            }
            else
            {
                if (context.ChatId != null)
                    BotClient.SendMessage(context.ChatId.Value, textMessage.ToString(),
                        parseMode: ParseMode.HTML, replyMarkup: rm, disableNotification: true);
            }
        }
    }
}