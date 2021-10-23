using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class LanguageRepository : EntityFrameworkCoreBaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
