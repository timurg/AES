using Telegram.BotAPI;

namespace MyStoryBot.Commands;

public abstract class BaseCommand
{
    protected readonly BotClient _botClient;

    public BaseCommand(BotClient botClient)
    {
        _botClient = botClient;
    }
}