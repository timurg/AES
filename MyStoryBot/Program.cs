using AES.BusinessLogic;
using AES.Infrastructure;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
var serviceProvider = ConfigureServices(configuration) ?? throw new ApplicationException();
if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

var botClient = new BotClient("6291851683:AAGz8AXfuHtNV1NcoAibWWuW3iuGDUumFNw");
var me = botClient.GetMe();
Console.WriteLine($"Start listening for @{me.Username}");

var updates = await botClient.GetUpdatesAsync();
Console.WriteLine("Enter to cycle");
while (true)
{
    Console.WriteLine("tic");
    if (updates.Any())
    {
        Console.WriteLine("Detect updates");
        foreach (var update in updates)
        {
            using (var unitOfWork = serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork)
            {
                try
                {
                    var userFinder = serviceProvider.GetService(typeof(IUserFinder)) as IUserFinder;
                    var user = userFinder.findByLogin(update.Message.From.Id.ToString());
                    var message = update.Message;
                    if (user == null)
                    {
                        botClient.SendMessage(message.Chat.Id, "Hello World!");
                    }
                    else
                    {
                        botClient.SendMessage(message.Chat.Id, $"Привет {user.Name}, ты написал:\n" + message.Text);
                    }
                    unitOfWork.Commit();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
        }

        var offset = updates.Last().UpdateId + 1;
        updates = botClient.GetUpdates(offset);
    }
    else
    {
        updates = botClient.GetUpdates();
    }
}

static IServiceProvider ConfigureServices(IConfigurationRoot configuration)
{
    var containerBuilder = new ContainerBuilder();
    containerBuilder.RegisterModule(new ConfigurationModule(configuration.GetSection("unitOfWorkFactory")));
    containerBuilder.Register(c => c.Resolve<IUnitOfWorkFactory>().Create()).SingleInstance();
    return new AutofacServiceProvider(containerBuilder.Build());
}