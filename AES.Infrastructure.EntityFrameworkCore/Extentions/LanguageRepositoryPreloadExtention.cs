using System.Linq;

namespace AES.Infrastructure.EntityFrameworkCore.Extentions
{

    /// <summary>
    /// Расширения для предзагрузки словаря языков
    /// </summary>
    public static class LanguageRepositoryPreloadExtention
    {
        /// <summary>
        /// Предварительно загрузить языки.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public static void PreloadLanguages(this UnitOfWork unitOfWork)
        {
            unitOfWork.LanguageRepository.GetQuery().ToList();
        }
    }
}
