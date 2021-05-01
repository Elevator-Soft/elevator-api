using Newtonsoft.Json;

namespace Models
{
    [JsonObject]
    public class User
    {
        public string Email { get; set; }

        public string Name { get; set; }
    }
}
