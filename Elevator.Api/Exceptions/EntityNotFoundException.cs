using System.Net;

namespace Elevator.Api.Exceptions
{
    public class EntityNotFoundException: ApiException
    {
        public EntityNotFoundException(string entityType, string id) : base(HttpStatusCode.NotFound, $"{entityType} with id '{id}' not found")
        { }
    }
}
