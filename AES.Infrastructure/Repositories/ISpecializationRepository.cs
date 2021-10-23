using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface ISpecializationRepository
    {
        Specialization Get(Guid id);
        void Save(Specialization entity);
        void Delete(Specialization entity);
        IQueryable<Specialization> GetQuery();
    }
}
