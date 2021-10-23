using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class SpecializationRepository : EntityFrameworkCoreBaseRepository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
