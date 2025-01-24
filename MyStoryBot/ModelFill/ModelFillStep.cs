using System;
using NLog;
using Telegram.BotAPI;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace MyStoryBot.ModelFill;

public abstract class ModelFillStep : StepBody
{
    protected readonly ITelegramBotClient _botClient;
    protected readonly ILogger _logger;

    public string Input { get; set; }
    public long ChatId { get; set; }
    public string PropertyName { get; set; }
    
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var data = context.PersistenceData as ModelFillData;
        
        if (string.IsNullOrEmpty(Input))
        {
            await RequestPropertyValue();
            return ExecutionResult.Suspend();
        }

        if (!ValidateInput(Input))
        {
            await SendError("Неверное значение");
            return ExecutionResult.Suspend();
        }

        StoreValue(data, Input);
        return ExecutionResult.Next();
    }

    protected abstract Task RequestPropertyValue();
    protected abstract bool ValidateInput(string input);
    protected abstract void StoreValue(ModelFillData data, string input);
}