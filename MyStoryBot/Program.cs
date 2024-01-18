using System.Collections.Concurrent;
using System.Net.Mime;
using System.Text;
using AES.BusinessLogic;
using AES.BusinessLogic.Implementation;
using AES.Domain;
using AES.Infrastructure;
using AES.Story;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyStoryBot;
using MyStoryBot.Commands;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableMethods.FormattingOptions;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using Telegram.BotAPI.UpdatingMessages;
using Module = AES.Domain.Module;
NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>()
    .Build();
var serviceProvider = ConfigureServices(configuration) as AutofacServiceProvider ?? throw new ApplicationException();
if (serviceProvider == null) throw new ArgumentNullException(paramName: "serviceProvider");

var botClient = new BotClient(configuration["bot:id"]);

var me = botClient.GetMe();
_logger.Debug($"Start listening for @{me.Username}");

var updates = await botClient.GetUpdatesAsync();
_logger.Debug("Enter to cycle");
botClient.SetMyCommands(new BotCommand("info", "Информация"), new BotCommand("/next", "Далее"));
var unitOfWorkFactory = serviceProvider.GetService(typeof(IUnitOfWorkFactory)) as IUnitOfWorkFactory;

var namedCommands = new BlockingCollection<NamedCommand>();
namedCommands.Add(new StartCommand(botClient));
namedCommands.Add(new InfoCommand(botClient));
namedCommands.Add(new NextCommand(botClient));
namedCommands.Add(new AllResultsCommand(botClient));

namedCommands.Add(new ShowCommand(botClient));
namedCommands.Add(new StartLearningCommand(botClient));

namedCommands.Add(new PollAnswerCommand(botClient));

while (true)
{
    _logger.Trace("tic");
    if (updates.Any())
    {
        _logger.Debug("Detect updates");
        foreach (var update in updates)
        {
            using var unitOfWork = unitOfWorkFactory.Create();
            try
            {
                var userFinder = new UserFinder(unitOfWork); //serviceProvider.GetService(typeof(IUserFinder)) as IUserFinder;
                var commandContext = update.GetCommandContext(unitOfWork, userFinder);
                namedCommands.ExecuteCommand(commandContext);
            }
            catch (NextStepException exception)
            {
                //botClient.SendMessage(ch,
                //    $"Привет {user.Name}, ты написал:\n" + message.Text);
                _logger.Error(exception.ToString());
            }
            catch (Exception exception)
            {
                _logger.Error(exception.ToString());
            }
                
            unitOfWork.Commit();
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
