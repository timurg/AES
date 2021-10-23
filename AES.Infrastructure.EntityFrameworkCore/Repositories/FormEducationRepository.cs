using AES.Domain;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class FormEducationRepository : EntityFrameworkCoreBaseRepository<FormEducation>,
        IFormEducationRepository
    {
        public FormEducationRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}
