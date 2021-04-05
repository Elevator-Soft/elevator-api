using Newtonsoft.Json;

namespace Elevator.Api.Configuration
{
    [JsonObject]
    public class JwtBearerConfiguration
    {
        public string Authority { get; set; }

        public string Audience { get; set; }
    }
}
