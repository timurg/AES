using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class RateEducationRepository : EntityFrameworkCoreBaseRepository<RateEducation>, IRateEducationRepository
    {
        public RateEducationRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
