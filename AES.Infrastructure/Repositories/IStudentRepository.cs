using System;
using System.Linq;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IStudentRepository
    {
        Student Get(Guid id);
        void Save(Student entity);
        void Delete(Student entity);
        IQueryable<Student> GetQuery();
    }
}
