using System.Text;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableMethods.FormattingOptions;

namespace MyStoryBot.Commands;

public class ShowCommand : CallbackQueryCommand
{
    public ShowCommand(BotClient botClient) : base(botClient, "show", false)
    {
    }

    protected override void ExecutionCallBackContext(CommandContext context)
    {
        var id = new Guid(context.Parameters[1]);
        var person = context.UnitOfWork.PersonRepository.GetQuery().First(s => s.Student.Id == id);
        var items
            = person.Student.Curriculum.Modules.First().Items;
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
        _botClient.SendMessage(context.ChatId.Value, textMessage.ToString(),
            parseMode: ParseMode.HTML, disableNotification: true);
    }
}