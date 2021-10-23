namespace AES.Domain
{
    public class Specialization : DictionaryElement
    {
        public Specialization(System.Guid nId, string nName, string nShortName, string nAbbreviation)
            : base(nId, nName, nShortName, nAbbreviation)
        {
        }

        public Specialization()
        {
        }
    }
}
