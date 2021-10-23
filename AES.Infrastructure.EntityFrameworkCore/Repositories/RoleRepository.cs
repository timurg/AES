using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class RoleRepository : EntityFrameworkCoreBaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
