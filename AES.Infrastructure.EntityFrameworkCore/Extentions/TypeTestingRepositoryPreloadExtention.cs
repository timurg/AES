using System.Linq;

namespace AES.Infrastructure.EntityFrameworkCore.Extentions
{
    public static class TypeTestingRepositoryPreloadExtention
    {
        public static void PreloadTypeTesting(this UnitOfWork unitOfWork)
        {
            unitOfWork.TypeTestingRepository.GetQuery().ToList();
        }
    }
}
