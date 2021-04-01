using System.Threading.Tasks;
using Repositories.Database.Models;
using Repositories.Repositories;

namespace Elevator.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository repository;

        public ProjectService(ProjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Example> CreateAsync()
        {
            return await repository.AddAsync(new Example());
        }
    }
}
