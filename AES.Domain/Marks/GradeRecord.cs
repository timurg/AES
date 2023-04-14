using System;
using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    public abstract class GradeRecord : DomainObject
    {
        [Required]
        public DateTimeOffset GradeDateTime { get; set; }

        [Required]
        public bool IsPassed { get; set; }

        public abstract string Description { get; }
    }
}
