﻿using AES.Story;
using Telegram.BotAPI;

namespace MyStoryBot.Commands;

public class PollAnswerCommand : NamedCommand
{
    public PollAnswerCommand(BotClient botClient) : base(botClient, "pollanswer")
    {
    }

    public override void ExecutionContext(CommandContext context)
    {
        var poolId = context.Parameters[0];
        var person = context.UnitOfWork.PersonRepository.GetQuery().FirstOrDefault(p =>
            p.Student.Curriculum.Modules.Any(a =>
                a.Items.Any(b =>
                    ((MyStory)b.LearningProcess).Items.Any(item =>
                        ((StoryPoll)item).ObjectId == poolId))));
        var storyPollItem = person.Student.Curriculum.Modules
            .SelectMany(m => m.Items)
            .Where(m => m.LearningProcess is MyStory)
            .SelectMany(m => ((MyStory)m.LearningProcess).Items).Where(i => i is StoryPoll)
            .First(i => ((StoryPoll)i).ObjectId == poolId) as StoryPoll;

        var moduleItem = person.Student.Curriculum.Modules
            .SelectMany(m => m.Items)
            .Where(m => m.LearningProcess is MyStory)
            .First(i => ((MyStory)i.LearningProcess).Items.Any(i => i.Id == storyPollItem.Id));

      //  storyPollItem.SelectedItem = update.PollAnswer.OptionIds.First();
        storyPollItem.CheckAnswer();

     //   ProcessLearning(context, processStep, context.User.Student,
     //       moduleItem);
    }
}