using System.Threading.Tasks;
using Elevator.Api.Dto;
using Elevator.Api.Models;
using Repositories.Repositories;
using Project = Elevator.Api.Models.Project;

namespace Elevator.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository repository;

        public ProjectService(ProjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            var dbProject = await repository.AddAsync(ConvertServiceModelToDbModel(project));
            return ConvertDbModelToServiceModel(dbProject);
        }

        public Project ConvertDbModelToServiceModel(Repositories.Database.Models.Project dbProject) => new Project
        {
            Id = dbProject.Id,
            Name = dbProject.Name,
            GitToken = dbProject.GitToken,
            ProjectUri = dbProject.ProjectUri
        };


        public Repositories.Database.Models.Project ConvertServiceModelToDbModel(Project project) => new Repositories.Database.Models.Project
        {
            Id = project.Id,
            Name = project.Name,
            GitToken = project.GitToken,
            ProjectUri = project.ProjectUri
        };
    }
}
