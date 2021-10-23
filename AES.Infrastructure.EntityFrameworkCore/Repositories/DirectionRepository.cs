using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class DirectionRepository : EntityFrameworkCoreBaseRepository<Direction>,
        IDirectionRepository
    {
        public DirectionRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
