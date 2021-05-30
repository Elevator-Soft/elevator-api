using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Elevator.Api.Dto
{
    [JsonObject]
    public class GrantAccessRequestDto
    {
        public Guid ProjectId { get; set; }
        
        public string UserId { get; set; }

        public AccessType AccessType { get; set; }

        public override string ToString()
        {
            return $"{nameof(ProjectId)}: {ProjectId}\n" +
                   $"{nameof(UserId)}: {UserId}\n" + 
                   $"{nameof(AccessType)}: {AccessType}";
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AccessType
    {
        User,
        Admin
    }
}
