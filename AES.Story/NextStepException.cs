using AES.Domain;

namespace AES.Story;

public class NextStepException : ApplicationException
{
    public MyStory Story { get; }

    public NextStepException(MyStory story) : base("Вы не можете сделать следующий шаг, не выполнив текущий.")
    {
        Story = story;
        
    }
}