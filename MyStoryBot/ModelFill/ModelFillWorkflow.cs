using System;
using WorkflowCore.Interface;

namespace MyStoryBot.ModelFill;

public class ModelFillWorkflow : IWorkflow<ModelFillData>
{
    public string Id => "ModelFillWorkflow";
    public int Version => 1;

    public void Build(IWorkflowBuilder<ModelFillData> builder)
    {
        builder
            .StartWith<InitializeModelStep>()
                .Input(step => step.ChatId, data => data.ChatId)
            .Then<RequestPropertyStep>()
                .Input(step => step.PropertyName, _ => "Name")
            .Then<RequestPropertyStep>()
                .Input(step => step.PropertyName, _ => "Age")
            .Then<ValidateModelStep>()
            .If(data => data.IsValid)
                .Then<SaveModelStep>()
            .EndWorkflow();
    }
}