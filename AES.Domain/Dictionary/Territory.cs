namespace AES.Domain
{
    public class Territory : DictionaryElement
    {
        public Territory(System.Guid nId, string nName, string nShortName, string nAbbreviation)
            : base(nId, nName, nShortName, nAbbreviation)
        {
        }

        public Territory()
        {
        }
    }
}
