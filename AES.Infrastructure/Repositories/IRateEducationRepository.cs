using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IRateEducationRepository
    {
        RateEducation Get(Guid id);
        void Save(RateEducation entity);
        void Delete(RateEducation entity);
        IQueryable<RateEducation> GetQuery();
    }
}
