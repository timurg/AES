using AES.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class EntityFrameworkCoreBaseRepository<T> where T : DomainObject
    {
        public AESEntityFrameworkCoreContext Context { get; private set; }

        public EntityFrameworkCoreBaseRepository(AESEntityFrameworkCoreContext context)
        {
            Context = context;
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual T Get(Guid id)
        {
            if (Context.Set<T>().Local.Any(e => e.Id == id))
            {
                return Context.Set<T>().Find(id);
            }
            else
            {
                return (from e in GetQuery()
                        where e.Id == id
                        select e).FirstOrDefault();
            }
        }

        public virtual IQueryable<T> GetQuery()
        {
            return Context.Set<T>();
        }

        public virtual void Save(T entity)
        {
            if (Context.Set<T>().Any(e => e.Id == entity.Id))
            {
                Context.Set<T>().Attach(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                Context.Set<T>().Add(entity);
            }
        }
    }
}
