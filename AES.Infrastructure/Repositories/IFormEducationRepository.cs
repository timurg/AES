using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IFormEducationRepository
    {
        FormEducation Get(Guid id);
        void Save(FormEducation entity);
        void Delete(FormEducation entity);
        IQueryable<FormEducation> GetQuery();
    }
}

