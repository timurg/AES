using AES.Domain;

namespace AES.BusinessLogic;

public interface IEducationProcessStep
{
    void Process(Student student, ModuleItem moduleItem);
}