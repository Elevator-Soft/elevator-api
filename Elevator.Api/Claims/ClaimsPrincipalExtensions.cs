using System.Security.Claims;
using Elevator.Api.Exceptions;

namespace Elevator.Api.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetValueByType(this ClaimsPrincipal claims, string type)
        {
            var claim = claims.FindFirst(type);

            if (claim == null)
                throw new ClaimNotFoundException(type);

            return claim.Value;
        }
    }
}
