using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AES.Domain
{
    public class Curator : DomainObject
    {
        public ICollection<CuratorDescription> Descriptions { get; set; } = new HashSet<CuratorDescription>();

        [Required]
        public Person Person { get; set; }
    }
}
