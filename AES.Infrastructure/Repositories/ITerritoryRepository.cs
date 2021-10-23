using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface ITerritoryRepository
    {
        Territory Get(Guid id);
        void Save(Territory entity);
        void Delete(Territory entity);
        IQueryable<Territory> GetQuery();
    }
}
