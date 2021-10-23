using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    public class Organization : DomainObject
    {
        public Organization(System.Guid nId, string nName, string nShortName, string nAbbreviation)
            : base(nId)
        {
            Abbreviation = nAbbreviation;
            Name = nName;
            ShortName = nShortName;
        }
        public ICollection<Subdivision> Subdivisions { get; set; }
        public Organization()
        {
            Subdivisions = new HashSet<Subdivision>();
        }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(50)]
        public string Abbreviation { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(50)]
        public string ShortName { get; set; }
    }
}
