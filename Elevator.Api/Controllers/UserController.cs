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
        public Task<HttpOperationResult<User>> GetCurrentUserAsync()
        {
            return Task.FromResult(HttpOperationResult<User>.Ok(GetUser()));
        }
    }
}
