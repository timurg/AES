using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface ITypeTestingRepository
    {
        TypeTesting Get(Guid id);
        void Save(TypeTesting entity);
        void Delete(TypeTesting entity);
        IQueryable<TypeTesting> GetQuery();
    }
}
