using System.Threading.Tasks;
using Elevator.Api.Models;

namespace Elevator.Api.Services
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(Project project);
    }
}
