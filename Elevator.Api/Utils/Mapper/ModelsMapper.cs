using Elevator.Api.Models;

namespace Elevator.Api.Utils.Mapper
{
    public static class ModelsMapper
    {
        public static Project ConvertProjectDbModelToServiceModel(Repositories.Database.Models.Project dbProject) => new Project
        {
            Id = dbProject.Id,
            Name = dbProject.Name,
            GitToken = dbProject.GitToken,
            ProjectUri = dbProject.ProjectUri
        };


        public static Repositories.Database.Models.Project ConvertProjectServiceModelToDbModel(Project project) => new Repositories.Database.Models.Project
        {
            Id = project.Id,
            Name = project.Name,
            GitToken = project.GitToken,
            ProjectUri = project.ProjectUri
        };
    }
}
