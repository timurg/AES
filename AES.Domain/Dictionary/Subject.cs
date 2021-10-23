namespace AES.Domain
{
    public abstract class Subject : DictionaryElement
    {
        public Subject(System.Guid nId, string nName, string nShortName, string nAbbreviation)
            : base(nId, nName, nShortName, nAbbreviation)
        {
        }

        public Subject()
        {
        }
    }
}
