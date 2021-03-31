using System.Threading.Tasks;
using Repositories.Database.Models;
using Repositories.Repositories;

namespace Elevator.Api.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly ProjectsRepository repository;

        public ProjectsService(ProjectsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Example> CreateAsync()
        {
            return await repository.AddAsync(new Example());
        }
    }
}
