using AES.BusinessLogic;
using AES.Infrastructure;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;

namespace MyStoryBot.Commands;

public static class CommandExtensions
{
    public static void ExecuteCommand(this IEnumerable<NamedCommand> namedCommands,
        CommandContext commandContext)
    {
        var command = namedCommands.FirstOrDefault(c => c.CommandName == commandContext.CommandName);
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

    public static CommandContext GetCommandContext(this Update update, IUnitOfWork unitOfWork, IUserFinder userFinder)
    {
        var commandContext = new CommandContext();
        commandContext.UnitOfWork = unitOfWork;
        //serviceProvider.GetService(typeof(IUserFinder)) as IUserFinder;

        User telegramUser = null;

        if (update.Message != null)
        {
            telegramUser = update.Message.From;

            commandContext.FromId = update.Message.From.Id;
            commandContext.ChatId = update.Message.Chat.Id;
        }
        else if (update.CallbackQuery != null)
        {
            commandContext.FromId = update.CallbackQuery.From.Id;
            commandContext.ChatId = update.CallbackQuery.Message.Chat.Id;
        }
        else if (update.PollAnswer != null)
        {
            commandContext.FromId = update.PollAnswer.User.Id;
        }

        commandContext.User = userFinder.findByLogin(commandContext.FromId.ToString());

        if (commandContext.User == null)
        {
            commandContext.User = UserUtils.InitNewUser(unitOfWork, commandContext.FromId.Value, telegramUser);
        }

        {
            var message = update.Message;
            if (message != null)
            {
                commandContext.MessageId = message.MessageId;
                commandContext.Message = message.Text;
                var hasText = !string.IsNullOrEmpty(commandContext.Message);
                if (hasText)
                {
                    if (commandContext.Message.Equals("Далее", StringComparison.OrdinalIgnoreCase))
                    {
                        commandContext.Message = "/next";
                    }

                    if (commandContext.Message.StartsWith('/'))
                    {
                        var commandParts = commandContext.Message.Split(' ');
                        if (commandParts.Any())
                        {
                            commandContext.CommandName = commandParts[0].Trim().ToLower().Remove(0, 1);
                            commandContext.Parameters = commandParts.Skip(1).ToArray();
                        }
                    }
                }
            }
            else if (update.PollAnswer != null)
            {
                commandContext.CommandName = "pollanswer";
                var par = new List<string> { update.PollAnswer.PollId };
                par.AddRange(update.PollAnswer.OptionIds.Select(c => c.ToString()));
                commandContext.Parameters = par.ToArray();
            }
            else if (update.CallbackQuery != null)
            {
                var data = update.CallbackQuery.Data;
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var commandData = data.Split(' ');
                    commandContext.CommandName = commandData[0].Trim().ToLower();
                    var callBackParams = new List<string>(commandData.Skip(1).ToArray());
                    callBackParams.Insert(0, update.CallbackQuery.Id);
                    commandContext.Parameters = callBackParams.ToArray();
                }
            }
        }
        return commandContext;
    }
}