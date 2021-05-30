using System.Net;

namespace Common
{
    public class VoidOperationResult
    {
        public bool IsSuccessful { get; protected init; } = true;
        public string Error { get; protected init; }

        public static VoidOperationResult Success() => new()
        { };

        public static VoidOperationResult Failed(string error = "") => new()
        {
            Error = error,
            IsSuccessful = false
        };
    }

    public class HttpVoidOperationResult: VoidOperationResult
    {
        public HttpStatusCode HttpStatusCode { get; private init; }

        public static HttpVoidOperationResult CreateBadOperationResult(HttpStatusCode httpStatusCode, string errorMessage) =>
            new()
            {
                IsSuccessful = false,
                HttpStatusCode = httpStatusCode,
                Error = errorMessage
            };

        public static HttpVoidOperationResult CreateGoodOperationResult(HttpStatusCode httpStatusCode) =>
            new()
            {
                IsSuccessful = true,
                HttpStatusCode = httpStatusCode
            };

        public static HttpVoidOperationResult Ok() => CreateGoodOperationResult(HttpStatusCode.OK);

        public static HttpVoidOperationResult BadRequest(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.BadRequest, $"Bad request. Error message: {errorMessage}");

        public static HttpVoidOperationResult Forbidden(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.Forbidden, $"Forbidden. Error message: {errorMessage}");

        public static HttpVoidOperationResult NotFound(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.NotFound, $"Not found. Error message: {errorMessage}");

        public static HttpVoidOperationResult InternalServerError(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.InternalServerError,
                $"Internal server error, smth went wrong. Error message: {errorMessage}");
    }
}
