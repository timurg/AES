using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class QualificationRepository : EntityFrameworkCoreBaseRepository<Qualification>, IQualificationRepository
    {
        public QualificationRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
