using System.ComponentModel.DataAnnotations;

namespace AES.Domain.Course;

public class BaseCourseElement : DomainObject
{
    [Required]
    public string Title { get; set; }
    
}