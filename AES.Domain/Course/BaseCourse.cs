using System.ComponentModel.DataAnnotations;

namespace AES.Domain.Course;

public class BaseCourse : DomainObject
{
    [Required]
    public string Name { get; set; }
}