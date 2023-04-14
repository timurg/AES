using AES.Domain;

namespace AES.Story;

public class MyStoryTemplateQuizItem : DomainObject
{
    public string Content { get; set; }
    public string Explanation { get; set; }
    public bool IsCorrect { get; set; }
    public uint? Order { get; set; }
}