using System;
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
            //this.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString) ;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //optionsBuilder.LogTo(System.Console.WriteLine);
        }
    }
}
