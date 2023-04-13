namespace AES.Domain;

public abstract class LearningProcess : DomainObject
{
    public abstract bool BeginLearning();
    public abstract bool CanEnd();
    public abstract bool EndLearning();
    public abstract void ResetLearning();
    public abstract bool IsStarted();
}