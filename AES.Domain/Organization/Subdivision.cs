using System;
using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    public class Subdivision : DomainObject
    {
        public Subdivision(System.Guid nId, string nName, string nShortName, string nAbbreviation)
            : base(nId)
        {
            Abbreviation = nAbbreviation;
            Name = nName;
            ShortName = nShortName;
        }
        public Subdivision()
        {

        }

        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

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
