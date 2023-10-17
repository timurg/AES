using AES.Domain;

namespace AES.Domain;

//Контекст выполнения команды
public record CommandContext
{
    public long? ChatId;
    public int? MessageId;
    public long? FromId;
    public string Message;
    public Person User;
    
}