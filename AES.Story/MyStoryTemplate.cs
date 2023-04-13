using System.Collections.ObjectModel;
using AES.Domain;

namespace AES.Story;

public class MyStoryTemplate : DomainObject
{
    
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<MyStoryTemplateItem> Items { get; set; } = new Collection<MyStoryTemplateItem>();
}