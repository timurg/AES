using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace AES.Infrastructure.EntityFrameworkCore.PostgreSql
{

    /// <summary>
    /// Класс для работы с Migrations
    /// </summary>
    public sealed class PostgreSqlConnectionFactory : IDesignTimeDbContextFactory<AESEntityFrameworkCorePostgreSqlContext>
    {
        public AESEntityFrameworkCorePostgreSqlContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
            var connectionString = configuration.GetConnectionString("aes");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new System.Exception("Nya!");
            }
            return new AESEntityFrameworkCorePostgreSqlContext(connectionString);
        }
    }
}
