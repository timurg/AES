using AES.Domain;
using AES.Infrastructure;

namespace MyStoryBot.Commands;

//Контекст выполнения команды
public record CommandContext
{
    public string CommandName = null!;
    public long? ChatId;
    public int? MessageId;
    public long? FromId;
    public string Message = null!;
    public Person User = null!;
    public IUnitOfWork UnitOfWork = null!;
    public string[] Parameters = null!;
}