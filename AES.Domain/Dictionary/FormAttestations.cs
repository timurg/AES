namespace AES.Domain
{
    public class FormAttestations : DictionaryElement
    {
        public FormAttestations(System.Guid nId, string nName, string nShortName, string nAbbreviation)
            : base(nId, nName, nShortName, nAbbreviation)
        {
        }

        public FormAttestations()
        {
        }
    }
}
