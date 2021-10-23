using System;
using AES.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class OrganizationRepository : EntityFrameworkCoreBaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }

        public override IQueryable<Organization> GetQuery()
        {
            return Context.Set<Organization>().Include(p => p.Subdivisions).AsQueryable();
        }
    }
}
