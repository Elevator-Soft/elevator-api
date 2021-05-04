using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Elevator.Api.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync();
        Task<Project> CreateAsync(Project project);
    }
}
