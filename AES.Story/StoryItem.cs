using AES.Domain;

namespace AES.Story;

public abstract class StoryItem : DomainObject
{
    public int? TelegramId { get; set; }
}