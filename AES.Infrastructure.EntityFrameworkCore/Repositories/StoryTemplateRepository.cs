using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using AES.Story;

namespace AES.Infrastructure.EntityFrameworkCore;

public class StoryTemplateRepository : EntityFrameworkCoreBaseRepository<MyStoryTemplate>, IStoryTemplateRepository
{
    public StoryTemplateRepository(AESEntityFrameworkCoreContext context) : base(context)
    {
    }
    
    public override IQueryable<MyStoryTemplate> GetQuery()
    {
        return Context.Set<MyStoryTemplate>()
            .Include(p => p.Items)
            .AsQueryable();
    }
}