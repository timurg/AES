using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite;

public class PostgreSqlConnectionFactory : IDesignTimeDbContextFactory<AESEntityFrameworkCoreSqliteContext>
{
    public AESEntityFrameworkCoreSqliteContext CreateDbContext(string[] args)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets(assembly).Build();
        var connectionString = configuration.GetConnectionString("aes");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new System.Exception("Nya!");
        }
        return new AESEntityFrameworkCoreSqliteContext(connectionString);
    }
    
}