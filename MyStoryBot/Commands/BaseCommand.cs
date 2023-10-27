using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.UpdatingMessages;

namespace MyStoryBot.Commands;

public abstract class BaseCommand
{
    protected readonly BotClient _botClient;

    public BaseCommand(BotClient botClient)
    {
        _botClient = botClient;
    }
    
    protected void DeleteUserCommand(CommandContext commandContext)
    {
        _botClient.DeleteMessage(commandContext.ChatId.Value, commandContext.MessageId.Value);
    }

    public void SendTextMessage(CommandContext commandContext, string message, bool disableNotification = false)
    {
        _botClient.SendMessage(commandContext.ChatId.Value,
            message, disableNotification: true);
    }
}