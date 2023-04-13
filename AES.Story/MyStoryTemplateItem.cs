using AES.Domain;

namespace AES.Story;

public abstract class MyStoryTemplateItem : DomainObject
{
    public int ItemIndex { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public abstract StoryItem CreateStoryItem();
}