using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elevator.Api.Authentication;
using Elevator.Api.Dto;
using Elevator.Api.Exceptions;
using Elevator.Api.Services.Interfaces;
using Models;
using Elevator.Api.Utils.Mapper;
using Repositories.Repositories;

namespace Elevator.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectRepository projectRepository;
        private readonly UserRepository userRepository;

        public ProjectService(ProjectRepository projectRepository, UserRepository userRepository)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
        }

        public async Task<List<Project>> GetAllAsync(AuthenticatedUser currentUser)
        {
            var user = ModelsMapper.ConvertUserDatabaseModelToService(await userRepository.FindByIdAsync(currentUser.Id));
            var dbProjects = await projectRepository.GetAllAsync();
            return dbProjects
                .Where(x => user.ProjectAccesses.User.Contains(x.Id.ToString()))
                .Select(ModelsMapper.ConvertProjectDbModelToServiceModel)
                .ToList();
        }

        public async Task<Project> CreateAsync(Project project, AuthenticatedUser currentUser)
        {
            var dbProject = await projectRepository.AddAsync(ModelsMapper.ConvertProjectServiceModelToDbModel(project));

            var dbUser = await userRepository.FindByIdAsync(currentUser.Id);
            dbUser.ProjectsWithAdminAccess.Add(dbProject.Id.ToString());
            dbUser.ProjectsWithUserAccess.Add(dbProject.Id.ToString());
            await userRepository.UpdateAsync(dbUser);

            return ModelsMapper.ConvertProjectDbModelToServiceModel(dbProject);
        }

        public async Task<Project> GetByIdAsync(Guid id, AuthenticatedUser currentUser)
        {
            await EnsureUserHasAccessAsync(currentUser, id, AccessType.User, nameof(GetByIdAsync));
            var dbProject = await projectRepository.FindByIdAsync(id);
            if (dbProject == null)
                throw new EntityNotFoundException(nameof(Project), id.ToString());
            return ModelsMapper.ConvertProjectDbModelToServiceModel(dbProject);
        }

        public async Task<Project> UpdateAsync(Project project, AuthenticatedUser currentUser)
        {
            await EnsureUserHasAccessAsync(currentUser, project.Id, AccessType.Admin, nameof(UpdateAsync));
            var dbProject = await projectRepository.FindByIdAsync(project.Id);
            if (dbProject == null)
                throw new EntityNotFoundException(nameof(Project), project.Id.ToString());

            dbProject.Name = project.Name;
            dbProject.GitToken = project.GitToken;
            dbProject.ProjectUri = project.ProjectUri;

            await projectRepository.UpdateAsync(dbProject);
            return ModelsMapper.ConvertProjectDbModelToServiceModel(dbProject);
        }

        public async Task GrantAccess(Guid projectId, AuthenticatedUser currentAuthenticatedUser, string targetUserId, AccessType accessType)
        {
            await EnsureUserHasAccessAsync(currentAuthenticatedUser, projectId, AccessType.Admin, nameof(GrantAccess));
            var dbProject = await projectRepository.FindByIdAsync(projectId);
            if (dbProject == null)
                throw new EntityNotFoundException(nameof(Project), projectId.ToString());
            var dbTargetUser = await userRepository.FindByIdAsync(targetUserId);
            if (dbTargetUser == null)
                throw new EntityNotFoundException(nameof(User), targetUserId);
            switch (accessType)
            {
                case AccessType.User:
                    dbTargetUser.ProjectsWithUserAccess.Add(projectId.ToString());
                    break;
                case AccessType.Admin:
                    dbTargetUser.ProjectsWithUserAccess.Add(projectId.ToString());
                    dbTargetUser.ProjectsWithAdminAccess.Add(projectId.ToString());
                    break;
            }

            await userRepository.UpdateAsync(dbTargetUser);
        }

        private async Task EnsureUserHasAccessAsync(AuthenticatedUser currentAuthenticatedUser, Guid projectId, AccessType accessType, string actionName)
        {
            var user = ModelsMapper.ConvertUserDatabaseModelToService(await userRepository.FindByIdAsync(currentAuthenticatedUser.Id));
            switch (accessType)
            {
                case AccessType.User:
                    if (!user.ProjectAccesses.User.Contains(projectId.ToString()))
                        throw new ForbiddenException(actionName);
                    break;
                case AccessType.Admin:
                    if (!user.ProjectAccesses.Admin.Contains(projectId.ToString()))
                        throw new ForbiddenException(actionName);
                    break;
            }
        }
    }
}
