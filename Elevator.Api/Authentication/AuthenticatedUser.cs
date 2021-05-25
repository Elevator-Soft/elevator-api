using Newtonsoft.Json;

namespace Elevator.Api.Authentication
{
    [JsonObject]
    public class AuthenticatedUser
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
