using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using AES.Infrastructure.EntityFrameworkCore.Extentions;

namespace AES.Infrastructure.EntityFrameworkCore.PostgreSql
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public AESEntityFrameworkCorePostgreSqlContext Context { get; private set;}

        public UnitOfWorkFactory(string connectionString)
        {
            Context = new AESEntityFrameworkCorePostgreSqlContext(connectionString);
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
}
