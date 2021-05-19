using System.Net;
using System.Text;

namespace Common
{
    public class OperationResult<T>
    {
        public T Value { get; protected init; }
        public bool IsSuccessful { get; protected init; } = true;
        public string Error { get; protected init; }

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

    public class HttpOperationResult<T> : OperationResult<T>
    {
        public HttpStatusCode HttpStatusCode { get; private init; }

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
