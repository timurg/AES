using AES.Domain;

namespace AES.Story;

public class StoryPollItem : DomainObject
{
    public string Content { get; set; }
    public string Explanation { get; set; }
    public bool IsCorrect { get; set; }
    public int Order { get; set; }
}