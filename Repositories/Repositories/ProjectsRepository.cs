using Repositories.Database;
using Repositories.Database.Models;

namespace Repositories.Repositories
{
    public class ProjectsRepository : AbstractRepository<Example>
    {
        public ProjectsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
