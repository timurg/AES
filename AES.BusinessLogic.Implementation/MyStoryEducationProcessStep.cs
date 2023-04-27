using AES.Domain;
using AES.Infrastructure;
using AES.Story;

namespace AES.BusinessLogic.Implementation;

public class MyStoryEducationProcessStep : BusinessObject, IEducationProcessStep
{
    public ProcessState Process(Student student, ModuleItem moduleItem)
    {
        if (moduleItem.LearningProcess != null)
        {
            var story = moduleItem.LearningProcess as MyStory;
            if (moduleItem.LearningProcess.IsStarted())
            {
                if (moduleItem.LearningProcess.CanEnd())
                {
                    moduleItem.Grade = moduleItem.LearningProcess.EndLearning();
                    moduleItem.GradeRecords.Add(moduleItem.Grade);
                    return ProcessState.AutoEnding;
                }
            }
        }

        return ProcessState.None;
    }

    public MyStoryEducationProcessStep(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}