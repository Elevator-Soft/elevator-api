using System;
using Newtonsoft.Json;

namespace Elevator.Api.Dto
{
    [JsonObject]
    public class CreateProjectRequestDto
    {
        public string Name { get; set; }
        public Uri GitUrl { get; set; }
        public string GitToken { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}\n" +
                   $"{nameof(GitUrl)}: {GitUrl}\n" +
                   $"{nameof(GitToken)}: " + $"{(string.IsNullOrEmpty(GitToken) ? "empty" : "secret")}";
        }
    }
}
