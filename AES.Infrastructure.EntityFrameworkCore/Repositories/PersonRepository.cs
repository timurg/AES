using AES.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using AES.Story;

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
                .Include(p => p.Student).
                ThenInclude(s => s.Curriculum).
                ThenInclude(c => c.Modules)
                .ThenInclude(m => m.Items)
                .ThenInclude(m => m.LearningProcess)
                .ThenInclude(e => ((MyStory)e).StoryTemplate.Items).ThenInclude(e => ((MyStoryTemplateQuiz)e).Items)
                .Include(p => p.Student).
                ThenInclude(s => s.Curriculum).
                ThenInclude(c => c.Modules)
                .ThenInclude(m => m.Items)
                .ThenInclude(m => m.LearningProcess).ThenInclude(e => ((MyStory)e).Items)
                .Include(p => p.Curator).
                ThenInclude(t => t.Descriptions)
                .Include(p => p.Tutor)
                .ThenInclude(t => t.Descriptions)
                .Include(p => p.Student).
                ThenInclude(s => s.Curriculum).
                ThenInclude(c => c.Modules)
                .ThenInclude(m => m.Items)
                .ThenInclude(m => m.Grade)
                .Include(p => p.Student).
                ThenInclude(s => s.Curriculum).
                ThenInclude(c => c.Modules)
                .ThenInclude(m => m.Items)
                .ThenInclude(m => m.GradeRecords)
                .AsQueryable();
        }
    }
}
