using System;
using System.Linq;
using AES.Domain;
using AES.Infrastructure;
using AES.Story;

namespace AES.BusinessLogic.Implementation;

public class MyStoryLearningProcessBuilder : BusinessObject, ILearningProcessBuilder
{
    public LearningProcess CreateLearningProcess(Student student, ModuleItem moduleItem)
    {
        if (moduleItem.LearningProcess == null)
        {

            var template = UnitOfWork.StoryTemplateRepository.GetQuery().First(t => 
                t.Subject == moduleItem.Subject 
                && t.TypeTesting == moduleItem.TypeTesting
                && t.Semester == moduleItem.Semester);
            
            var story = new MyStory
            {
                StoryTemplate = template,
                StoryStep = -1
            };
            return story;
        }
        else
        {
            throw new ApplicationException("Ошибка при создании процесса обучения");
        }
    }

    public MyStoryLearningProcessBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}