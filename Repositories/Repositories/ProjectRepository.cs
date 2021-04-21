using Repositories.Database;
using Repositories.Database.Models;

namespace Repositories.Repositories
{
    public class ProjectRepository : AbstractRepository<Project>
    {
        public ProjectRepository(DatabaseContext dbContext) : base(dbContext, dbContext.Projects)
        {
        }
    }
}
