using AES.Domain;

namespace AES.Exceptions;

public class EndLearningException : ApplicationException
{
    public EndLearningException(LearningProcess learningProcess, string message = "") : base(string.IsNullOrEmpty(message) ? $"Can't end learning process {learningProcess.Id}" : message)
    {
        LearningProcess = learningProcess;
    }

    public LearningProcess LearningProcess { get; }
}