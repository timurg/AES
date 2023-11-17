using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using AES.Domain;
using AES.Exceptions;
using AES.Story.Exceptions;

namespace AES.Story;

public class MyStory : LearningProcess
{
    public override bool BeginLearning()
    {
        if (IsStarted())
        {
            throw new ApplicationException("Обучение запущено.");
        }

        if (!StoryTemplate.Items.Any())
        {
            throw new StoryTemplateIsEmptyException(this);
        }
        StoryGeneration++;
        InternalNextStep();
        return true;
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
            var testItems = GetCurrentGenerationItems().OfType<StoryPoll>().ToArray();
            var passCount = testItems.Length / 2;
            var record = new BalledGradeRecord
            {
                
                IsPassed = (!testItems.Any()) || (testItems.Count(i => i.IsPassed.HasValue &&  i.IsPassed.Value) >= passCount),
                GradeDateTime = DateTimeOffset.Now,
                Id = Guid.NewGuid(),
                
            };
            ResetLearning();
            return record;
        }
        
        throw new EndLearningException(this,"Cant close learning process");
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
    
    public StoryItem? NextStep()
    {
        if (!IsStarted()) throw new LearningProcessNotStartedException(this);
        var currentStep = GetCurrentStoryItem();
        if (currentStep is StoryPoll { IsPassed: null })
        {
            throw new NextStepException(this);
        }
        return StoryStep >= StoryTemplate.Items.Count - 1 ? null : InternalNextStep();
    }

    public int StoryStep { get; set; }

    public uint StoryGeneration { get; protected set; } 
    
    public IList<StoryItem> Items { get; } = new Collection<StoryItem>();

    public IEnumerable<StoryItem> GetCurrentGenerationItems()
    {
        return Items.Where(i => i.Generation == StoryGeneration).ToArray();
    }

    public MyStoryTemplate StoryTemplate { get; set; } = null!;

    public StoryItem GetCurrentStoryItem()
    {
        return Items.OrderBy(p => p.DateCreated).Last();
    }
}