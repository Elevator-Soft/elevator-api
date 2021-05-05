using Models;
using Microsoft.AspNetCore.Mvc;
using Elevator.Api.Claims;

namespace Elevator.Api.Controllers
{
    public abstract class ControllerBase: Controller
    {
        protected User GetUser()
        {
            return new User
            {
                Email = User.GetValueByType(ClaimTypes.Email),
                Name = User.GetValueByType(ClaimTypes.Name)
            };
        }
    }
}
