using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elevator.Api.Models;
using Elevator.Api.Utils.Mapper;
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

        public async Task<List<Project>> GetAllAsync()
        {
            var dbProjects = await repository.GetAllAsync();
            return dbProjects
                .Select(ModelsMapper.ConvertProjectDbModelToServiceModel)
                .ToList();
        }

        public async Task<Project> CreateAsync(Project project)
        {
            var dbProject = await repository.AddAsync(ModelsMapper.ConvertProjectServiceModelToDbModel(project));
            return ModelsMapper.ConvertProjectDbModelToServiceModel(dbProject);
        }

    }
}
