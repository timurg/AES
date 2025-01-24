using System;
using MyStoryBot.Commands;
using Telegram.BotAPI;
using WorkflowCore.Interface;

namespace MyStoryBot.ModelFill;

public abstract class BaseModelFillCommand<T> : NamedCommand where T : class, new()
{
    protected readonly IWorkflowHost _workflowHost;
    private readonly IWorkflowRepository _workflowRepository;

    protected BaseModelFillCommand(TelegramBotClient botClient, string commandName) : base(botClient, commandName)
    {
    }

    public override async Task ExecuteAsync(CommandContext context)
    {
        var existingWorkflow = await _workflowRepository.GetWorkflowInstance(
            context.User.Id.ToString());

        if (existingWorkflow == null)
        {
            var workflowId = await StartNewWorkflow(context);
            return;
        }

        if (context.CallbackQuery?.Data == "cancel")
        {
            await _workflowHost.TerminateWorkflow(existingWorkflow.Id);
            await _botClient.SendTextMessageAsync(context.ChatId, "Заполнение отменено");
            return;
        }

        await _workflowHost.PublishEvent(
            existingWorkflow.Id, 
            "UserInput", 
            context.Text);
    }

    private async Task<string> StartNewWorkflow(CommandContext context)
    {
        var data = new ModelFillData 
        { 
            ChatId = context.ChatId,
            UserId = context.User.Id
        };

        return await _workflowHost.StartWorkflow("ModelFillWorkflow", data);
    }
}