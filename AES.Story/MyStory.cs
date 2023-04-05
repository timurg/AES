using System.Collections.ObjectModel;
using AES.Domain;

namespace AES.Story;

public class MyStory : ModuleItem
{
    public override bool BeginLeanrning()
    {
        throw new NotImplementedException();
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
    
    public int StoryStep { get; set; }

    public IList<StoryItem> Items { get; } = new Collection<StoryItem>();
}