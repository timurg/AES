using AES.BusinessLogic;
using AES.BusinessLogic.Implementation;
using AES.Exceptions;
using AES.Story;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace MyStoryBot.Commands;

public class NextCommand : NamedCommand
{
    public NextCommand(BotClient botClient) : base(botClient, "next")
    {
    }

    public override void ExecutionContext(CommandContext context)
    {
        var eduItem = context.User.Student.Curriculum.Modules.First().Items.FirstOrDefault(i =>
            (i.LearningProcess != null) &&
            (i.LearningProcess.IsStarted()));
        if (eduItem != null)
        {
            IEducationProcessStep processStep = new MyStoryEducationProcessStep(context.UnitOfWork);
            var isStarted = eduItem.LearningProcess != null;
            MyStory story;
            StoryItem storyItem;
            if (!isStarted)
            {
                ILearningProcessBuilder builder = new MyStoryLearningProcessBuilder(context.UnitOfWork);
                eduItem.LearningProcess = builder.CreateLearningProcess(context.User.Student, eduItem);
                eduItem.LearningProcess.BeginLearning();
                story = eduItem.LearningProcess as MyStory;
                storyItem = story.GetCurrentStoryItem();
            }
            else
            {
                story = eduItem.LearningProcess as MyStory;
                try
                {
                    storyItem = story.NextStep();
                } catch (LearningProcessNotStartedException)
                {
                    storyItem = null;
                }
            }

            RenderItemStory(context, story, storyItem);
            ProcessLearning(context, processStep, context.User.Student, eduItem);
            DeleteUserCommand(context);
        }
        else
        {
            DeleteUserCommand(context);
            BotClient.SendMessage(context.ChatId.Value,
                $"Наберите команду /info");
        }
    }
}