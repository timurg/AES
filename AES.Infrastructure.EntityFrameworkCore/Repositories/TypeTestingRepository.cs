using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class TypeTestingRepository : EntityFrameworkCoreBaseRepository<TypeTesting>, ITypeTestingRepository
    {
        public TypeTestingRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
