using AES.Domain;

namespace AES.BusinessLogic;

public interface ILearningProcessBuilder
{
    LearningProcess CreateLearningProcess(Student student, ModuleItem moduleItem);
}