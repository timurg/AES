using AES.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class PersonRepository : EntityFrameworkCoreBaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }

        public override IQueryable<Person> GetQuery()
        {
            return Context.Set<Person>()
                .Include(p => p.Student).ThenInclude(s => s.Curriculum).ThenInclude(c => c.Modules).ThenInclude(m => m.Items)
                .Include(p => p.Curator).ThenInclude(t => t.Descriptions)
                .Include(p => p.Tutor).ThenInclude(t => t.Descriptions).AsQueryable();
        }
    }
}
