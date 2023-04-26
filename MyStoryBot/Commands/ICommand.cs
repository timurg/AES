using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace MyStoryBot.Commands;

public interface ICommand
{
    void Execute();
}