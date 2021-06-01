using System.Text.Json.Serialization;

namespace Models
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BuildStatus
    {
        WaitingToGetPlaceInQueue,
        WaitingToStart,
        InProgress,
        Success,
        Failed
    }
}
