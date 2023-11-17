using AES.Domain;

namespace AES.Exceptions;

public class LearningProcessNotStartedException : ApplicationException
{
    public LearningProcessNotStartedException(LearningProcess learningProcess) : base($"Learning process {learningProcess.Id} not started.")
    {
        LearningProcess = learningProcess;
    }

    public LearningProcess LearningProcess { get; }
}