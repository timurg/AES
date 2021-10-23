using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface ISubjectRepository
    {
        Subject Get(Guid id);
        void Save(Subject entity);
        void Delete(Subject entity);
        IQueryable<Subject> GetQuery();
    }
}
