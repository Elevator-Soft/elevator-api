using Elevator.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Elevator.Api.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Example> example { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfig = new DatabaseConfig();
            var config = dbConfig.ReadConfig();
            if (config != null)
                optionsBuilder.UseNpgsql(config);
        }
    }
}
