using Microsoft.EntityFrameworkCore;

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite;

public class AESEntityFrameworkCoreSqliteContext: AESEntityFrameworkCoreContext
{
    private string connectionString;

    public AESEntityFrameworkCoreSqliteContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString);
        optionsBuilder.LogTo(System.Console.WriteLine);
    }
}