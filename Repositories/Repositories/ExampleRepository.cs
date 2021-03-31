using Repositories.Database;
using Repositories.Database.Models;

namespace Repositories.Repositories
{
    public class ExampleRepository : AbstractRepository<Example>
    {
        public ExampleRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
