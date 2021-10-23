using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IQualificationRepository
    {
        Qualification Get(Guid id);
        void Save(Qualification entity);
        void Delete(Qualification entity);
        IQueryable<Qualification> GetQuery();
    }
}
