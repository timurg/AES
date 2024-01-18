namespace AES.Story;

/// <summary>
/// Часть истории, опрос, тест пользователя
/// </summary>
public class StoryPoll: StoryTextContentBase
{
    public IList<StoryPollItem> Items { get; set; } = new List<StoryPollItem>();
    public uint? SelectedItem { get; set; }
    public void CheckAnswer()
    {
        var poolTemplate = Template as MyStoryTemplateQuiz;
        IsPassed = SelectedItem == poolTemplate.Items.First(i => i.IsCorrect).Order;
    }
}