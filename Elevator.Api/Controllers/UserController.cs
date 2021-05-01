using System.Threading.Tasks;
using Common;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace Elevator.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController: ControllerBase
    {
        [HttpGet("me")]
        public Task<OperationResult<User>> GetCurrentUserAsync()
        {
            return Task.FromResult(OperationResult<User>.Ok(GetUser()));
        }
    }
}
