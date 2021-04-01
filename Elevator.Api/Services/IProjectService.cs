using System.Threading.Tasks;
using Elevator.Api.Models;
using Project = Elevator.Api.Models.Project;

namespace Elevator.Api.Services
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(CreateProjectRequest createProjectRequest);
    }
}
