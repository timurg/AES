namespace AES.Story;

public abstract class MyStoryTemplateFileBased : MyStoryTemplateItem
{
    public string ContentType { get; set; }
    public string FileName { get; set; }
    public string? TelegramFileId { get; set; }
}