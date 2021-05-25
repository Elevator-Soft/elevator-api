using System.Threading.Tasks;
using Models;

namespace Elevator.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(string id);

        Task RegisterUser(User user);

        Task<bool> ExistsAsync(string id);
    }
}
