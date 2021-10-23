using AES.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class SubjectRepository : EntityFrameworkCoreBaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }

        //public override IQueryable<Subject> GetQuery()
        //{
        //    return Context.Set<Subject>().Include(p => p.).Include(p => p.Curator).ThenInclude(t => t.Descriptions).Include(p => p.Tutor).ThenInclude(t => t.Descriptions).AsQueryable();
        //}

    }
}
