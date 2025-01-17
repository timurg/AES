using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;

namespace MyStoryBot.Commands;

public class AllResultsCommand : NamedCommand
{
    public AllResultsCommand(TelegramBotClient botClient) : base(botClient, "allresults")
    {
    }

    public override void ExecutionContext(CommandContext context)
    {
        if (context.User.Roles.Any(r => r.Name == "admin"))
        {
            foreach (var student in context.UnitOfWork.StudentRepository.GetQuery().ToArray())
            {
                var btn = new InlineKeyboardButton[1];
                btn[0] = new InlineKeyboardButton("Показать");
                var rm = new InlineKeyboardMarkup(new[]
                    {
                        btn
                    }
                );
                btn[0].CallbackData = "show " + student.Id;
                if (context.ChatId != null && student.Person != null)
                    BotClient.SendMessage(context.ChatId.Value, $"{student.Person.Login}: {student.Person.FullName}",
                        parseMode: "HTML", replyMarkup: rm, disableNotification: true);
            }
        }
        else
        {
            if (context.ChatId != null)
                BotClient.SendMessage(context.ChatId.Value,
                    $"Недостаточно прав для выполнения команды.");
        }
    }
}