using System.Collections.ObjectModel;
using AES.Domain;

namespace AES.Story;

public class MyStory : LearningProcess
{
    public override bool BeginLearning()
    {
        if (IsStarted())
        {
            throw new ApplicationException("Обучение запущено.");
        }
        StoryGeneration++;
        return InternalNextStep() != null;
    }

    public override bool CanEnd()
    {
        return IsLastStoryStep() &&
               !Items.Any(i => i.Generation == StoryGeneration && !i.IsPassed.HasValue);
    }

    public override GradeRecord EndLearning()
    {
        if (CanEnd())
        {
            var record = new BalledGradeRecord
            {
                IsPassed = !Items.Any(i => i.Generation == StoryGeneration && i.IsPassed.HasValue && !i.IsPassed.Value),
                GradeDateTime = DateTimeOffset.Now,
                Id = Guid.NewGuid()
            };
            ResetLearning();
            return record;
        }
        else
        {
            throw new ApplicationException("Cant close learning process");
        }
    }

    public override void ResetLearning()
    {
        StoryStep = -1;
    }

    public override bool IsStarted()
    {
        return StoryStep >= 0;
    }

    private bool IsLastStoryStep()
    {
        return StoryStep == (StoryTemplate.Items.Count - 1);
    }

    private StoryItem InternalNextStep()
    {
        StoryStep++;
        var newItem = StoryTemplate.Items.First(p => p.ItemIndex == StoryStep).CreateStoryItem();
        newItem.ItemIndex = Items.Count(i => i.Generation == StoryGeneration);
        newItem.Generation = StoryGeneration;
        Items.Add(newItem);
        return newItem;
    }
    
    public StoryItem NextStep()
    {
        if (!IsStarted()) return null;
        if (StoryStep >= StoryTemplate.Items.Count - 1) return null;
        return InternalNextStep();
    }

    public int StoryStep { get; set; }

    public uint StoryGeneration { get; protected set; } 
    
    public IList<StoryItem> Items { get; } = new Collection<StoryItem>();
    
    public MyStoryTemplate StoryTemplate { get; set; }

    public StoryItem GetCurrentStoryItem()
    {
        return Items.OrderBy(p => p.DateCreated).Last();
    }
}