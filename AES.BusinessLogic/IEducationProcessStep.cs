using AES.Domain;

namespace AES.BusinessLogic;

public interface IEducationProcessStep
{
    ProcessState Process(Student student, ModuleItem moduleItem);
}