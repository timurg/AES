using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class TerritoryRepository : EntityFrameworkCoreBaseRepository<Territory>, ITerritoryRepository
    {
        public TerritoryRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
