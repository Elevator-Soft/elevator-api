using System.Threading.Tasks;
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

        public async Task<Project> CreateAsync(CreateProjectRequest createProjectRequest)
        {
            var dbProject = await repository.AddAsync(ConvertCreateProjectRequestToDbModel(createProjectRequest));
            return ConvertDbModelToApiModel(dbProject);
        }

        public Project ConvertDbModelToApiModel(Repositories.Database.Models.Project dbProject) => new Project
        {
            Id = dbProject.Id,
            Name = dbProject.Name
        };

        public Repositories.Database.Models.Project ConvertCreateProjectRequestToDbModel(
            CreateProjectRequest createProject) =>
            new Repositories.Database.Models.Project
            {
                Name = createProject.Name,
                GitToken = createProject.GitToken,
                ProjectUri = createProject.ProjectUri
            };

    }
}
