using AES.Domain;

namespace AES.Story;

public class MyStoryTemplateQuizItem : DomainObject
{
    public string Content { get; set; } = default!;
    public string Explanation { get; set; } = default!;
    public bool IsCorrect { get; set; }
    public int? Order { get; set; }
}