using System.Net;

namespace Common
{
    public class OperationResult<T>
    {
        public T Value { get; private init; }
        public bool IsSuccessful { get; private init; } = true;
        public HttpStatusCode HttpStatusCode { get; private init; }
        public string Error { get; private init; }

        public static OperationResult<T> CreateBadOperationResult(HttpStatusCode httpStatusCode, string errorMessage) => new OperationResult<T>
        {
            IsSuccessful = false,
            HttpStatusCode = httpStatusCode,
            Error = errorMessage
        };

        public static OperationResult<T> CreateGoodOperationResult(HttpStatusCode httpStatusCode, T value) => new OperationResult<T>
        {
            HttpStatusCode = httpStatusCode,
            Value = value
        };

        public static OperationResult<T> Ok(T value) => CreateGoodOperationResult(HttpStatusCode.OK, value);

        public static OperationResult<T> Created(T value) => CreateGoodOperationResult(HttpStatusCode.Created, value);

        public static OperationResult<T> BadRequest(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.BadRequest, $"Bad request. Error message: {errorMessage}");

        public static OperationResult<T> Forbidden(string errorMessage = "") => 
            CreateBadOperationResult(HttpStatusCode.Forbidden, $"Forbidden. Error message: {errorMessage}");

        public static OperationResult<T> NotFound(string errorMessage = "") => 
            CreateBadOperationResult(HttpStatusCode.NotFound, $"Not found. Error message: {errorMessage}");

        public static OperationResult<T> InternalServerError(string errorMessage = "") => 
            CreateBadOperationResult(HttpStatusCode.InternalServerError, $"Internal server error, smth went wrong. Error message: {errorMessage}");
    }
}
