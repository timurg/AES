using AES.BusinessLogic;
using AES.BusinessLogic.Implementation;
using AES.Story;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace MyStoryBot.Commands;

public class StartLearningCommand : CallbackQueryCommand
{
    public StartLearningCommand(BotClient botClient) : base(botClient, "startlearning", true, 
        "Используйте кнопку \"Далее\" рядом с клавиатурой, для навигации.")
    {
    }

    protected override void ExecutionCallBackContext(CommandContext context)
    {
        var id = new Guid(context.Parameters[1]);
        IEducationProcessStep processStep = new MyStoryEducationProcessStep(context.UnitOfWork);
        var eduItem = context.User.Student.Curriculum.Modules.SelectMany(s => s.Items)
            .First(s => s.Id == id);
        var isStarted = eduItem.LearningProcess != null &&
                        eduItem.LearningProcess.IsStarted();
        if (!isStarted)
        {
            ILearningProcessBuilder builder = new MyStoryLearningProcessBuilder(context.UnitOfWork);
            if (eduItem.LearningProcess == null)
            {
                eduItem.LearningProcess =
                    builder.CreateLearningProcess(context.User.Student, eduItem);
            }

            eduItem.LearningProcess.BeginLearning();
            var story = eduItem.LearningProcess as MyStory;
            var storyItem = story.GetCurrentStoryItem();
                                        
            RenderItemStory(context, story, storyItem);
            ProcessLearning(context, processStep, context.User.Student, eduItem);
        }
        
    }
    
    
}