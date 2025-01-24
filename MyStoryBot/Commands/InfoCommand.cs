using System.Text;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;

namespace MyStoryBot.Commands;

public class InfoCommand : NamedCommand
{
    public InfoCommand(TelegramBotClient botClient) : base(botClient, "info")
    {
    }

    public override Task ExecuteAsync(CommandContext context)
    {
        throw new NotImplementedException();
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
            btn[0] = new InlineKeyboardButton("Начать");
            var rm = new InlineKeyboardMarkup(new[] {btn});
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
                        parseMode: "HTML", disableNotification: true);
            }
            else
            {
                if (context.ChatId != null)
                    BotClient.SendMessage(context.ChatId.Value, textMessage.ToString(),
                        parseMode: "HTML", replyMarkup: rm, disableNotification: true);
            }
        }
    }
}