using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface ILanguageRepository
    {
        Language Get(Guid id);
        void Save(Language entity);
        void Delete(Language entity);
        IQueryable<Language> GetQuery();
    }
}
