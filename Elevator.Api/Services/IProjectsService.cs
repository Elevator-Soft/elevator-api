using System.Threading.Tasks;
using Repositories.Database.Models;

namespace Elevator.Api.Services
{
    interface IProjectsService
    {
        Task<Example> CreateAsync();
    }
}
