using System;

namespace MyStoryBot.ModelFill;

public class ModelFillData
{
    public long UserId { get; set; }
    public long ChatId { get; set; }
    public string CurrentProperty { get; set; }
    public Dictionary<string, string> Values { get; set; } = new();
    public string Input { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsCancelled { get; set; }
}