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
        uint inx = 0;
        foreach (var option in options)
        {
            
            q.Items.Add(new MyStoryTemplateQuizItem()
            {
                Id = Guid.NewGuid(),
                Content = option,
                Order = inx,
                IsCorrect = inx == correctOptions,
                Explanation = string.Empty
            });
            inx++;
        }

        return q;
    }

    public override StoryPoll CreateStoryItem()
    {
        var pool = new StoryPoll
        {
            Content = Description,
            DateCreated = DateTimeOffset.Now,
            Id = Guid.NewGuid(),
            Template = this
        };
        foreach (var quizItem in Items)
        {
            pool.Items.Add(new StoryPollItem()
            {
                Content = quizItem.Content,
                Explanation = quizItem.Explanation,
                IsCorrect = quizItem.IsCorrect,
                Order = quizItem.Order,
               // Id = Guid.NewGuid()
            });
        }
        return pool;
    }
}