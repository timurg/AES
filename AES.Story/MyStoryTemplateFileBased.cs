namespace AES.Story;

public abstract class MyStoryTemplateFileBased : MyStoryTemplateItem
{
    public string ContentType { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string? TelegramFileId { get; set; }
}