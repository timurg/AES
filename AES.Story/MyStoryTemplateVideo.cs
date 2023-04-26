namespace AES.Story;

public class MyStoryTemplateVideo : MyStoryTemplateFileBased
{
    

    public override StoryItem CreateStoryItem()
    {
        var newItem = new StoryVideo()
        {
            Template = this,
            DateCreated = DateTimeOffset.Now,
        };
        return newItem;
    }

    public static MyStoryTemplateVideo CreateFromFile(string fileName, string title, Guid id = new(), int indexOrder = -1)
    {
        return new MyStoryTemplateVideo()
        {
            Id = id,
            Title = title,
            Description = "",
            ContentType = string.Empty,
            //Bits = File.ReadAllBytes(fileName),
            FileName =  Path.GetFileName(fileName),
            ItemIndex = indexOrder
        };
    }
}