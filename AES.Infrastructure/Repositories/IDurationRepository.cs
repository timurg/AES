using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IDurationRepository
    {
        Duration Get(Guid id);
        void Save(Duration entity);
        void Delete(Duration entity);
        IQueryable<Duration> GetQuery();
    }
}
