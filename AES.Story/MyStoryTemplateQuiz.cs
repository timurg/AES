namespace AES.Story;

public class MyStoryTemplateQuiz : MyStoryTemplateItem
{
    public IList<MyStoryTemplateQuizItem> Items { get; set; } = new List<MyStoryTemplateQuizItem>();

    public static MyStoryTemplateQuiz Create(string quizContent, string[] options, int correctOptions, 
        int indexOrder = -1, Guid id = new())
    {
        var q = new MyStoryTemplateQuiz
        {
            Id = id,
            Description = quizContent,
            ItemIndex = indexOrder,
            Title = "Выберите ваш вариант ответа"
        };
        var inx = 0;
        foreach (var option in options)
        {
            q.Items.Add(new MyStoryTemplateQuizItem()
            {
                Id = Guid.NewGuid(),
                Content = option,
                Order = indexOrder,
                IsCorrect = inx == correctOptions,
                Explanation = string.Empty
            });
        }

        return q;
    }

    public override StoryPoll CreateStoryItem()
    {
        var pool = new StoryPoll();
        
        return pool;
    }
}