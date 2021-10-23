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
        public virtual Person Person { get; set; }
    }
}
