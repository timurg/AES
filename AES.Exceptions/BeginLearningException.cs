using AES.Domain;

namespace AES.Exceptions;

/// <summary>
/// Ошибка возникающая при начале обучения 
/// </summary>
public class BeginLearningException : ApplicationException
{
    public BeginLearningException(LearningProcess learningProcess, string message = "") : base(string.IsNullOrEmpty(message) ? $"Can't start learning process {learningProcess.Id}" : message)
    {
        LearningProcess = learningProcess;
    }

    public LearningProcess LearningProcess { get; }
}