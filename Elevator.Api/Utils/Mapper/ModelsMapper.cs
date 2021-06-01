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

        public static BuildStep ConvertBuildStepDbModelToServiceModel(Repositories.Database.Models.BuildStep dbBuildStep) => new BuildStep
        {
            Id = dbBuildStep.Id,
            Name = dbBuildStep.Name,
            BuildConfigId = dbBuildStep.BuildConfigId,
            BuildStepScript = new BuildStepScript
            {
                Arguments = dbBuildStep.BuildStepScript.Arguments,
                Command = dbBuildStep.BuildStepScript.Command
            }
        };

        public static Repositories.Database.Models.BuildStep ConvertBuildStepServiceModelToDbModel(BuildStep buildStep) => new Repositories.Database.Models.BuildStep
        {
            Id = buildStep.Id,
            Name = buildStep.Name,
            BuildConfigId = buildStep.BuildConfigId,
            BuildStepScript = new Repositories.Database.Models.BuildStepScript
            {
                Arguments = buildStep.BuildStepScript.Arguments,
                Command = buildStep.BuildStepScript.Command
            }
        };

        public static BuildStepDto CovertBuildStepServiceModelToDto(BuildStep buildStep) => new BuildStepDto
        {
            Id = buildStep.Id,
            Name = buildStep.Name,
            BuildStepScript = buildStep.BuildStepScript
        };

        public static Build ConvertBuildDbModelToServiceModel(Repositories.Database.Models.Build dbBuild) => new Build
        {
            Id = dbBuild.Id,
            BuildConfigId = dbBuild.BuildConfigId,
            BuildStatus = dbBuild.BuildStatus,
            Logs = dbBuild.Logs,
            FinishTime = dbBuild.FinishTime,
            StartedByUserId = dbBuild.StartedByUserId,
            StartTime = dbBuild.StartTime
        };

        public static Repositories.Database.Models.Build ConvertBuildServiceModelToDbModel(Build build) => new Repositories.Database.Models.Build
        {
            Id = build.Id,
            BuildConfigId = build.BuildConfigId,
            BuildStatus = build.BuildStatus,
            Logs = build.Logs,
            FinishTime = build.FinishTime,
            StartedByUserId = build.StartedByUserId,
            StartTime = build.StartTime
        };

        public static BuildDto ConvertBuildServiceModelToDto(Build build) => new BuildDto
        {
            Id = build.Id,
            BuildConfigId = build.BuildConfigId,
            BuildStatus = build.BuildStatus,
            FinishTime = build.FinishTime,
            Logs = build.Logs,
            StartedByUserId = build.StartedByUserId
        };

        public static User ConvertUserDatabaseModelToService(Repositories.Database.Models.User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            ProjectAccesses = new()
            {
                Admin = user.ProjectsWithAdminAccess,
                User = user.ProjectsWithUserAccess
            }
        };

        public static Repositories.Database.Models.User ConvertUserServiceModelToDatabase(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            ProjectsWithAdminAccess = user.ProjectAccesses.Admin,
            ProjectsWithUserAccess = user.ProjectAccesses.User,
            IsRegistered = true
        };

        public static UserDto ConvertUserServiceModelToDto(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            IsRegistered = true,
            ProjectAccesses = new()
            {
                Admin = user.ProjectAccesses.Admin,
                User = user.ProjectAccesses.User
            }
        };
    }
}
