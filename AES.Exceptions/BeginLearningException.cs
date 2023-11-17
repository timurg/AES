using AES.Domain;

namespace AES.Exceptions;

public class BeginLearningException : ApplicationException
{
    public BeginLearningException(LearningProcess learningProcess, string message = "") : base(string.IsNullOrEmpty(message) ? $"Can't start learning process {learningProcess.Id}" : message)
    {
        LearningProcess = learningProcess;
    }

    public LearningProcess LearningProcess { get; }
}