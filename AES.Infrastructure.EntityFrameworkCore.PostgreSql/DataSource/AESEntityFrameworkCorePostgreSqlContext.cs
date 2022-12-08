using AES.Domain;
using Microsoft.EntityFrameworkCore;

namespace AES.Infrastructure.EntityFrameworkCore.PostgreSql
{
    public class AESEntityFrameworkCorePostgreSqlContext : AESEntityFrameworkCoreContext
    {
        private string connectionString;

        public AESEntityFrameworkCorePostgreSqlContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(System.Console.WriteLine);
        }
    }
}
