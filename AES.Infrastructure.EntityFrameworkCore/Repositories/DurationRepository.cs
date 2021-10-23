using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class DurationRepository : EntityFrameworkCoreBaseRepository<Duration>, IDurationRepository
    {
        public DurationRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
