using System.Security.Claims;
using Elevator.Api.Authentication;
using Elevator.Api.Claims;
using Models;
using ClaimTypes = Elevator.Api.Claims.ClaimTypes;

namespace Elevator.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static AuthenticatedUser GetServerUser(this ClaimsPrincipal user)
        {
            return new()
            {
                Id = user.GetValueByType(ClaimTypes.Email),
                Email = user.GetValueByType(ClaimTypes.Email),
                Name = user.GetValueByType(ClaimTypes.Name)
            };
        }
    }
}
