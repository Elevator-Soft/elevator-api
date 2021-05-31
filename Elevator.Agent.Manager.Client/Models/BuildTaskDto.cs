using Models;

namespace Elevator.Agent.Manager.Client.Models
{
    public sealed class BuildTaskDto
    {
        public string StartedByUserId { get; set; }

        public BuildConfig BuildConfig { get; set; }
    }
}
