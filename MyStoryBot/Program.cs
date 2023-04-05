using AES.BusinessLogic;
using AES.Infrastructure;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
var serviceProvider = ConfigureServices(configuration) ?? throw new ApplicationException();
if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

var botClient = new TelegramBotClient("6291851683:AAGz8AXfuHtNV1NcoAibWWuW3iuGDUumFNw");

using CancellationTokenSource cts = new ();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new ()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);



var me = await botClient.GetMeAsync();




Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' from user id {message.From.Id} message in chat {chatId}.");

    using (var unitOfWork = serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork)
    {
        var userFinder = serviceProvider.GetService(typeof(IUserFinder)) as IUserFinder;
        var user = userFinder.findByLogin(message.From.Id.ToString());
        if (user == null)
        {
            var sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Пользователь не найден",
                cancellationToken: cancellationToken);
        }
        else
        {
            var sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text:$"Привет {user.Name}, ты написал:\n" + messageText,
                cancellationToken: cancellationToken);
            /*
            var pollMessage = await botClient.SendPollAsync(
                chatId: chatId,
                question: "Did you ever hear the tragedy of Darth Plagueis The Wise?",
                options: new[]
                {
                    "Yes for the hundredth time!",
                    "No, who`s that?"
                },
                cancellationToken: cancellationToken);
            //var pool = pollMessage as Poll;
            //if (pool != null)
            Console.WriteLine($"pool  id: {pollMessage.MessageId}");*/
            var result = botClient.
        }
        unitOfWork.Commit();
    }

    // Echo received message text
    
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

static IServiceProvider ConfigureServices(IConfigurationRoot configuration)
{
    var containerBuilder = new ContainerBuilder();
    containerBuilder.RegisterModule(new ConfigurationModule(configuration.GetSection("unitOfWorkFactory")));
    containerBuilder.Register(c => c.Resolve<IUnitOfWorkFactory>().Create()).SingleInstance();
    return new AutofacServiceProvider(containerBuilder.Build());
}