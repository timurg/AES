using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class StudentRepository : EntityFrameworkCoreBaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
