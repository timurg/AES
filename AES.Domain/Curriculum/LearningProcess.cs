namespace AES.Domain;

/// <inheritdoc />
public abstract class LearningProcess : DomainObject
{
    public abstract bool BeginLearning();
    public abstract bool CanEnd();
    public abstract GradeRecord EndLearning();
    public abstract void ResetLearning();
    public abstract bool IsStarted();
}