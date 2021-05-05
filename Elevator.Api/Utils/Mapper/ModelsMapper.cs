using Elevator.Api.Dto;
using Models;

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

        public static ProjectDto ConvertProjectServiceModelToDtoModel(Project project) => new ProjectDto
        {
            Id = project.Id,
            Name = project.Name
        };

        public static BuildConfig ConvertBuildConfigDbModelToServiceModel(Repositories.Database.Models.BuildConfig dbBuildConfig) => new BuildConfig
        {
            Id = dbBuildConfig.Id,
            Name = dbBuildConfig.Name,
            ProjectId = dbBuildConfig.ProjectId
        };

        public static Repositories.Database.Models.BuildConfig ConvertBuildConfigServiceModelToDbModel(BuildConfig buildConfig) => new Repositories.Database.Models.BuildConfig
        {
            Id = buildConfig.Id,
            Name = buildConfig.Name,
            ProjectId = buildConfig.ProjectId
        };

        public static BuildConfigDto ConvertServiceBuildConfigModelToDto(BuildConfig buildConfig) => new BuildConfigDto
        {
            Id = buildConfig.Id,
            Name = buildConfig.Name
        };
    }
}
