using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elevator.Api.Authentication;
using Elevator.Api.Dto;
using Models;

namespace Elevator.Api.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync(AuthenticatedUser currentUser);
        Task<Project> CreateAsync(Project project, AuthenticatedUser currentUser);
        Task<Project> GetByIdAsync(Guid id, AuthenticatedUser currentUser);
        Task<Project> UpdateAsync(Project project, AuthenticatedUser currentUser);
        Task GrantAccess(Guid projectId, AuthenticatedUser currentAuthenticatedUser, string targetUserId, AccessType accessType);
    }
}
