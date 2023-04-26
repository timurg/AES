using System;
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
            var story = new MyStory
            {
                StoryTemplate = UnitOfWork.StoryTemplateRepository.Get(
                    new Guid("6A1DB7D6-40A5-4ABB-9D46-211A9D6F3420")),
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