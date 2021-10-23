using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{

    /// <summary>
    /// Описание курируемого направления тьютора
    /// </summary>
    public class TutorDescription : DomainObject
    {
        public Organization Organization { get; set; }
        public Subdivision Subdivision { get; set; }

        [Required]
        public Subject Subject { get; set; }
        public Direction Direction { get; set; }
        public Specialization Specialization { get; set; }

        [Required]
        public TypeTesting TypeTesting { get; set; }
        public int? Semester { get; set; }
        public bool IsSuperviser;
    }
}
