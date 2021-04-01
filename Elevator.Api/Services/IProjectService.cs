using System.Threading.Tasks;
using Repositories.Database.Models;

namespace Elevator.Api.Services
{
    public interface IProjectService
    {
        Task<Example> CreateAsync();
    }
}
