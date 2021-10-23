using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IRoleRepository
    {
        Role Get(Guid id);
        void Save(Role entity);
        void Delete(Role entity);
        IQueryable<Role> GetQuery();
    }
}
