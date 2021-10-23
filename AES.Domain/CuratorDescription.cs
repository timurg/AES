using System;
using System.Collections.Generic;
using System.Text;

namespace AES.Domain
{
    public class CuratorDescription : DomainObject
    {
        public Organization Organization { get; set; }
        public Subdivision Subdivision { get; set; }
        public Direction Direction { get; set; }
    }
}
