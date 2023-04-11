using System;
using System.Linq;
using AES.Story;

namespace AES.Infrastructure;

public interface IStoryTemplateRepository
{
    MyStoryTemplate Get(Guid id);
    void Save(MyStoryTemplate entity);
    void Delete(MyStoryTemplate entity);
    IQueryable<MyStoryTemplate> GetQuery();
}