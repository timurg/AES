using AES.Story;

namespace AES.Infrastructure.EntityFrameworkCore;

public class StoryTemplateRepository : EntityFrameworkCoreBaseRepository<MyStoryTemplate>, IStoryTemplateRepository
{
    public StoryTemplateRepository(AESEntityFrameworkCoreContext context) : base(context)
    {
    }
}