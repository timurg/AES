using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IOrganizationRepository
    {
        Organization Get(Guid id);
        void Save(Organization entity);
        void Delete(Organization entity);
        IQueryable<Organization> GetQuery();
    }
}
