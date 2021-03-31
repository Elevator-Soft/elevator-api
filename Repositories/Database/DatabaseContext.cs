using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Database.Models;

namespace Repositories.Database
{
    public sealed class DatabaseContext : DbContext
    {
        private readonly string connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Postgres");
        }

        public DbSet<Example> Example { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
