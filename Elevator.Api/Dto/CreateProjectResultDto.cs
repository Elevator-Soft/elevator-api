using System;
using Newtonsoft.Json;

namespace Elevator.Api.Dto
{
    [JsonObject]
    public class CreateProjectResultDto
    {
        public Guid Id { get; set; }
    }
}
