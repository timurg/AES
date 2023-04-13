using AES.Domain;

namespace AES.Story;

public abstract class StoryItem : DomainObject
{
    public DateTimeOffset DateCreated { get; set; }
    public int ItemIndex { get; set; }
    public int? TelegramId { get; set; }
}