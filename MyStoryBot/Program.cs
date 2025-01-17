using System.Collections.Concurrent;
using AES.BusinessLogic.Implementation;
using AES.Infrastructure;
using AES.Story;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyStoryBot.Commands;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;

NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
_logger.Debug($"Currnet directory: {Environment.CurrentDirectory}");

static void LogDirectoryStructure(string path, NLog.ILogger logger, string indent = "")
{
    logger.Debug($"{indent}Directory: {path}");
    
    // Вывод файлов
    foreach (var file in Directory.GetFiles(path))
    {
        var fileInfo = new FileInfo(file);
        logger.Debug($"{indent}├── {fileInfo.Name} ({fileInfo.Length:N0} bytes)");
    }

    // Рекурсивный обход поддиректорий
    foreach (var dir in Directory.GetDirectories(path))
    {
        LogDirectoryStructure(dir, logger, indent + "│   ");
    }
}

/*
// В методе Main после получения logger:
_logger.Debug("=== Directory Structure ===");
LogDirectoryStructure(Environment.CurrentDirectory, _logger);
_logger.Debug("=========================");
*/

var configuration = new ConfigurationBuilder()
    .AddJsonFile("./config/appsettings.json", optional: true)
    .AddUserSecrets<Program>()
    .Build();
var serviceProvider = ConfigureServices(configuration) as AutofacServiceProvider ?? throw new ApplicationException();
if (serviceProvider == null) throw new ArgumentNullException(paramName: "serviceProvider");

var botClient = new TelegramBotClient(botToken: configuration["bot:id"] ?? throw new ApplicationException("Requared parametr bot:id"));

var me = botClient.GetMe();
_logger.Debug($"Start listening for @{me.Username}");


var updates = await botClient.GetUpdatesAsync();
_logger.Debug("Enter to cycle");
SetMyCommandsArgs botCommands = new SetMyCommandsArgs(
new BotCommand[]
{
    new BotCommand("info", "Информация"),
    new BotCommand("/next", "Далее")
});
botClient.SetMyCommands(botCommands);
var unitOfWorkFactory = serviceProvider.GetService(typeof(IUnitOfWorkFactory)) as IUnitOfWorkFactory;

var namedCommands = new BlockingCollection<NamedCommand>
{
    new StartCommand(botClient),
    new InfoCommand(botClient),
    new NextCommand(botClient),
    new AllResultsCommand(botClient),
    new ShowCommand(botClient),
    new StartLearningCommand(botClient),
    new PollAnswerCommand(botClient)
};

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
