using Repositories.Database;
using Repositories.Database.Models;

namespace Repositories.Repositories
{
    public class ProjectRepository : AbstractRepository<Example>
    {
        public ProjectRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
