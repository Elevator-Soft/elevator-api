using Newtonsoft.Json;

namespace Models
{
    [JsonObject]
    public class User : BaseModel
    {
        public string Email { get; set; }
    }
}
