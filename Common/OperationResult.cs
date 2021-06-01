using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    [JsonObject]
    public class OperationResult<T>
    {
        [JsonProperty]
        public T Value { get; set; }

        [JsonProperty]
        public bool IsSuccessful { get; set; } = true;

        [JsonProperty]
        public string Error { get; set; }

        public static OperationResult<T> Success(T value) => new()
        {
            Value = value
        };

        public static OperationResult<T> Failed(string error = "") => new()
        {
            Error = error,
            IsSuccessful = false
        };
    }

    [JsonObject]
    public class HttpOperationResult<T> : OperationResult<T>
    {
        [JsonProperty]
        public HttpStatusCode HttpStatusCode { get; set; }

        public static HttpOperationResult<T> CreateBadOperationResult(HttpStatusCode httpStatusCode, string errorMessage) => new()
        {
            IsSuccessful = false,
            HttpStatusCode = httpStatusCode,
            Error = errorMessage
        };

        public static HttpOperationResult<T> CreateGoodOperationResult(HttpStatusCode httpStatusCode, T value) => new()
        {
            HttpStatusCode = httpStatusCode,
            Value = value
        };

        public static HttpOperationResult<T> Failed(HttpStatusCode statusCode, string errorMessage = "") => CreateBadOperationResult(statusCode, errorMessage);

        public static HttpOperationResult<T> Ok(T value) => CreateGoodOperationResult(HttpStatusCode.OK, value);

        public static HttpOperationResult<T> Created(T value) => CreateGoodOperationResult(HttpStatusCode.Created, value);

        public static HttpOperationResult<T> BadRequest(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.BadRequest, $"Bad request. Error message: {errorMessage}");

        public static HttpOperationResult<T> Forbidden(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.Forbidden, $"Forbidden. Error message: {errorMessage}");

        public static HttpOperationResult<T> NotFound(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.NotFound, $"Not found. Error message: {errorMessage}");

        public static OperationResult<T> InternalServerError(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.InternalServerError, $"Internal server error, smth went wrong. Error message: {errorMessage}");
    }
}
