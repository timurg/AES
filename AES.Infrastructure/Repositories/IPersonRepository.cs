using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IPersonRepository
    {
        Person Get(Guid id);
        void Save(Person entity);
        void Delete(Person entity);
        IQueryable<Person> GetQuery();
    }
}
