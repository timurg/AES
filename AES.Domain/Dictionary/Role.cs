using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    public class Role : DomainObject
    {
        protected string name;

        private Role()
            : base(System.Guid.Empty)
        {

        }

        public Role(System.Guid nId, string nName) : base(nId)
        {
            name = nName;
        }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(20)]
        public string Name { get { return name; } set { name = value; } }

        public bool match(Role val)
        {
            return (val != null) && (this.Id == val.Id);
        }
    }
}
