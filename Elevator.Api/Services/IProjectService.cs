using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Elevator.Api.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(Guid projectId);
        Task<Project> CreateAsync(Project project);
    }
}
