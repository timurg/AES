using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    public class DomainObject
    {
        protected System.Guid id;

        protected DomainObject()
        {
        }

        public DomainObject(System.Guid nID)
        {
            id = nID;
        }

        [Key]
        [Required]
        public System.Guid Id { get { return id; } set { id = value; } }
    }
}

