using AES.Infrastructure.EntityFrameworkCore.Extentions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    public AESEntityFrameworkCoreSqliteContext Context { get; private set;}

    public UnitOfWorkFactory(string connectionString)
    {
        Context = new AESEntityFrameworkCoreSqliteContext(connectionString);
        var serviceProvider = Context.GetInfrastructure<IServiceProvider>();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        //loggerFactory?.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
    }

    public IUnitOfWork Create()
    {
        var unitOfWork = new UnitOfWork(Context);

        //Предзагрузка словаря дисциплин
        unitOfWork.PreloadLanguages();
        unitOfWork.PreloadSubject();
        unitOfWork.PreloadTypeTesting();
        return unitOfWork;
    }
}