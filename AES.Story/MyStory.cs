using System.Collections.ObjectModel;
using AES.Domain;

namespace AES.Story;

public class MyStory : LearningProcess
{
    public override bool BeginLearning()
    {
        return NextStep() != null;
    }

    public override bool CanEnd()
    {
        throw new NotImplementedException();
    }

    public override bool EndLearning()
    {
        throw new NotImplementedException();
    }

    public override void ResetLearning()
    {
        throw new NotImplementedException();
    }

    public override bool IsStarted()
    {
        return StoryStep >= 0;
    }

    public StoryItem NextStep()
    {
        if (StoryStep >= StoryTemplate.Items.Count - 1) return null;
        StoryStep++;
        var newItem = StoryTemplate.Items.First(p => p.ItemIndex == StoryStep).CreateStoryItem();
        newItem.ItemIndex = Items.Count;
        Items.Add(newItem);
        return newItem;
    }
    
    public int StoryStep { get; set; }

    public IList<StoryItem> Items { get; } = new Collection<StoryItem>();
    
    public MyStoryTemplate StoryTemplate { get; set; }

    public StoryItem GetCurrentStoryItem()
    {
        return Items.OrderBy(p => p.DateCreated).Last();
    }
}