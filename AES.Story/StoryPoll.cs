namespace AES.Story;

public class StoryPoll: StoryItem
{
    public string Content { get; set; }
    public IList<StoryPollItem> Items { get; set; } = new List<StoryPollItem>();
}