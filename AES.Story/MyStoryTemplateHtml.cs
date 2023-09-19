namespace AES.Story;

public class MyStoryTemplateHtml : MyStoryTemplateItem
{

    public static MyStoryTemplateHtml Create(string title, string content, int indexOrder = -1, Guid id = new())
    {
        return new MyStoryTemplateHtml() { Id = id, Title = title, Description = content, ItemIndex = indexOrder};
    }

    public override StoryItem CreateStoryItem()
    {
        return new StoryHtml(){
            Content = Description,
            DateCreated = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Template = this
        };
    }
}