using AES.Domain;
using AES.Exceptions;

namespace AES.Story.Exceptions;

public class StoryTemplateIsEmptyException : BeginLearningException
{
    public StoryTemplateIsEmptyException(LearningProcess learningProcess) : base(learningProcess)
    {
    }
}