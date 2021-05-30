using System.Net;

namespace Elevator.Api.Exceptions
{
    public class ForbiddenException: ApiException
    {
        public ForbiddenException(string actionName) : base(HttpStatusCode.Forbidden, $"Access denied to action {actionName}")
        {
        }
    }
}
