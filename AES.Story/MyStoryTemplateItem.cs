using AES.Domain;

namespace AES.Story;

public abstract class MyStoryTemplateItem : DomainObject
{
    public string Title { get; set; }
    public string Description { get; set; }
}