using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace MyStoryBot.Commands;

public abstract class CallbackQueryCommand : NamedCommand
{
    private readonly bool _showAlert;
    private readonly string _alertMessage;

    public CallbackQueryCommand(BotClient botClient, string commandName, bool showAlert = false,
        string alertMessage = "") : base(botClient, commandName)
    {
        _showAlert = showAlert;
        _alertMessage = alertMessage;
    }

    public override void ExecutionContext(CommandContext context)
    {
        ExecutionCallBackContext(context);

        if (_showAlert)
        {
            _botClient.AnswerCallbackQuery(new AnswerCallbackQueryArgs(context.Parameters[0])
            {
                Text = _alertMessage, //"Используйте кнопку \"Далее\" рядом с клавиатурой, для навигации.", //update.CallbackQuery.Data,
                ShowAlert = _showAlert
            });
        }
        else
        {
            _botClient.AnswerCallbackQuery(new AnswerCallbackQueryArgs(context.Parameters[0])
            {
                ShowAlert = false
            });
        }
    }
    
    protected abstract void ExecutionCallBackContext(CommandContext context);
}