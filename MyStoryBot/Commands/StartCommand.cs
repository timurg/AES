using Telegram.BotAPI;

namespace MyStoryBot.Commands;

public class StartCommand: NamedCommand
{
    public StartCommand(BotClient botClient) : base(botClient, "start")
    {
    }

    public override void ExecutionContext(CommandContext context)
    {
        SendTextMessage(context,
            $"Привет {context.User.Name}, набери команду /info", disableNotification: true);
    }
}