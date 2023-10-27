namespace MyStoryBot.Commands;

public static class CommandExtensions
{
    public static void ExecuteCommand(this IEnumerable<NamedCommand> namedCommands, string commandName,
        CommandContext commandContext)
    {
        var command = namedCommands.FirstOrDefault(c => c.CommandName == commandName);
        if (command != null)
        {
            command.ExecutionContext(commandContext);
        }
        else
        {
            command.SendTextMessage(commandContext,
                $"Привет {commandContext.User.Name}, набери команду /info", disableNotification: true);
        }
    }
}