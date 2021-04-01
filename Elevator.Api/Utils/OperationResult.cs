using System.Net;

namespace Elevator.Api.Utils
{
    public class OperationResult<T>
    {
        public T Value { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Error { get; set; }

        public static OperationResult<T> Ok(T value) => new OperationResult<T>
        {
            Value = value,
            HttpStatusCode = HttpStatusCode.OK
        };

        public static OperationResult<T> Created(T value) => new OperationResult<T>
        {
            Value = value,
            HttpStatusCode = HttpStatusCode.Created
        };

        public static OperationResult<T> BadRequest(string errorMessage = "") => new OperationResult<T>
        {
            IsSuccessful = false,
            HttpStatusCode = HttpStatusCode.BadRequest,
            Error = $"Bad request. Error message: {errorMessage}"
        };

        public static OperationResult<T> Forbidden(string errorMessage = "") => new OperationResult<T>
        {
            IsSuccessful = false,
            HttpStatusCode = HttpStatusCode.Forbidden,
            Error = $"Forbidden. Error message: {errorMessage}"
        };

        public static OperationResult<T> NotFound(string errorMessage = "") => new OperationResult<T>
        {
            IsSuccessful = false,
            HttpStatusCode = HttpStatusCode.NotFound,
            Error = $"Not found. Error message: {errorMessage}"
        };

        public static OperationResult<T> InternalServerError(string errorMessage = "") => new OperationResult<T>
        {
            IsSuccessful = false,
            HttpStatusCode = HttpStatusCode.InternalServerError,
            Error = $"Internal server error, smth go wrong. Error message: {errorMessage}"
        };
    }
}
