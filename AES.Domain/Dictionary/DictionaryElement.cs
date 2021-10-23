using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    public abstract class DictionaryElement : DomainObject
    {
        protected string abbreviation;
        protected string name;
        protected string shortName;

        public DictionaryElement()
            : base(System.Guid.Empty)
        {

        }

        public DictionaryElement(System.Guid nId, string nName, string nShortName, string nAbbreviation)
            : base(nId)
        {
            abbreviation = nAbbreviation;
            name = nName;
            shortName = nShortName;
        }
        [Required(AllowEmptyStrings = true)]
        [MaxLength(255)]
        public string Abbreviation { get { return abbreviation; } set { abbreviation = value; } }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(255)]
        public string Name { get { return name; } set { name = value; } }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(50)]
        public string ShortName { get { return shortName; } set { shortName = value; } }

        public bool match(Direction val)
        {
            return (val != null) && (this.Id == val.Id);
        }
    }
}
