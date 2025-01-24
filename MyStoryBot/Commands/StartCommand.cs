using Telegram.BotAPI;

namespace MyStoryBot.Commands;

public class StartCommand: NamedCommand
{
    public StartCommand(TelegramBotClient botClient) : base(botClient, "start")
    {
    }

    public override Task ExecuteAsync(CommandContext context)
    {
        throw new NotImplementedException();
    }

    public override void ExecutionContext(CommandContext context)
    {
        SendTextMessage(context,
            $"Привет {context.User.Name}, набери команду /info", disableNotification: true);
    }
}