using Telegram.BotAPI;

namespace MyStoryBot.Commands;

public abstract class NamedCommand : BaseCommand
{
    public string CommandName { get; }

    public NamedCommand(BotClient botClient, string commandName) : base(botClient)
    {
        CommandName = commandName;
    }
    public abstract void ExecutionContext(CommandContext context);
}