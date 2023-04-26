using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using AES.Domain;

namespace AES.Story;

public class MyStoryTemplate : DomainObject
{
    [Required]
    public Subject Subject { get; set; }
    [Required]
    public TypeTesting TypeTesting { get; set; }
    [Required]
    public int Semester { get; set; }
    
    
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<MyStoryTemplateItem> Items { get; set; } = new Collection<MyStoryTemplateItem>();
}