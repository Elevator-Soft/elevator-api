using Elevator.Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Elevator.Api.Database
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<Example> Example { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
    }
}
