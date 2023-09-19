using System.Linq;
using AES.Domain;
using AES.Story;
using Microsoft.EntityFrameworkCore;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class StudentRepository : EntityFrameworkCoreBaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(AESEntityFrameworkCoreContext context) : base(context)
        {
        }
        
        public override IQueryable<Student> GetQuery()
        {
            return Context.Set<Student>()
                .Include(p => p.Person);
            /*.
            ThenInclude(s => s.Student.Curriculum).
            ThenInclude(c => c.Modules)
            .ThenInclude(m => m.Items)
            .ThenInclude(m => m.LearningProcess)
            .ThenInclude(e => ((MyStory)e).StoryTemplate.Items).ThenInclude(e => ((MyStoryTemplateQuiz)e).Items)
            .Include(p => p.Person).
            ThenInclude(s => s.Student.Curriculum).
            ThenInclude(c => c.Modules)
            .ThenInclude(m => m.Items)
            .ThenInclude(m => m.LearningProcess).ThenInclude(e => ((MyStory)e).Items)
            .Include(p => p.Person.Curator).
            ThenInclude(t => t.Descriptions)
            .Include(p => p.Person.Tutor)
            .ThenInclude(t => t.Descriptions)
            .Include(p => p.Person.Student).
            ThenInclude(s => s.Curriculum).
            ThenInclude(c => c.Modules)
            .ThenInclude(m => m.Items)
            .ThenInclude(m => m.Grade)
            .Include(p => p.Person.Student).
            ThenInclude(s => s.Curriculum).
            ThenInclude(c => c.Modules)
            .ThenInclude(m => m.Items)
            .ThenInclude(m => m.GradeRecords)
            .Include(p => p.Person.Roles)
            .AsQueryable();
            */
        }
    }
}
