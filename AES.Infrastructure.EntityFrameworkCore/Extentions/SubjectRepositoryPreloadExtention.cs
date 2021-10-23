using System.Linq;

namespace AES.Infrastructure.EntityFrameworkCore.Extentions
{
    public static class SubjectRepositoryPreloadExtention
    {
        public static void PreloadSubject(this UnitOfWork unitOfWork)
        {
            unitOfWork.SubjectRepository.GetQuery().ToList();
        }
    }
}
