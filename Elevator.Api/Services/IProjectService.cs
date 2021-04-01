using System.Threading.Tasks;
using Project = Elevator.Api.Models.Project;

namespace Elevator.Api.Services
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(Project project);
    }
}
