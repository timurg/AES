using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IDirectionRepository
    {
        Direction Get(Guid id);
        void Save(Direction entity);
        void Delete(Direction entity);
        IQueryable<Direction> GetQuery();
    }
}
