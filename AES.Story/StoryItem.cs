using AES.Domain;

namespace AES.Story;

public abstract class StoryItem : DomainObject
{
    public DateTimeOffset DateCreated { get; set; }
    public int ItemIndex { get; set; }
    public int? TelegramId { get; set; }
    public long? ChatId { get; set; }
    public string? ObjectId { get; set; }
    public MyStoryTemplateItem Template { get; set; }
    public bool? IsPassed { get; set; }
    public uint Generation { get; set; }
}