using AES.Domain.Course;

namespace AES.Infrastructure.EntityFrameworkCore;

public class BinaryDataRepository : EntityFrameworkCoreBaseRepository<BinaryData>, IBinaryDataRepository
{
    public BinaryDataRepository(AESEntityFrameworkCoreContext context) : base(context)
    {
    }
}