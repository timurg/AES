namespace AES.Domain
{
    public class Language : DictionaryElement
    {
        public Language(System.Guid nId, string nName, string nShortName, string nAbbreviation)
            : base(nId, nName, nShortName, nAbbreviation)
        {
        }

        public Language()
        {
        }
    }
}
