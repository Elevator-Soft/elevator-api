using System.Net;

namespace Elevator.Api.Utils
{
    public class VoidOperationResult
    {
        public bool IsSuccessful { get; set; } = true;
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Error { get; set; }

        public static VoidOperationResult
            CreateBadOperationResult(HttpStatusCode httpStatusCode, string errorMessage) => new VoidOperationResult
        {
            IsSuccessful = false,
            HttpStatusCode = httpStatusCode,
            Error = errorMessage
        };

        public static VoidOperationResult CreateGoodOperationResult(HttpStatusCode httpStatusCode) =>
            new VoidOperationResult
            {
                IsSuccessful = true,
                HttpStatusCode = httpStatusCode
            };

        public static VoidOperationResult Ok() => CreateGoodOperationResult(HttpStatusCode.OK);

        public static VoidOperationResult BadRequest(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.BadRequest, $"Bad request. Error message: {errorMessage}");

        public static VoidOperationResult Forbidden(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.Forbidden, $"Forbidden. Error message: {errorMessage}");

        public static VoidOperationResult NotFound(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.NotFound, $"Not found. Error message: {errorMessage}");

        public static VoidOperationResult InternalServerError(string errorMessage = "") =>
            CreateBadOperationResult(HttpStatusCode.InternalServerError,
                $"Internal server error, smth went wrong. Error message: {errorMessage}");
    }
}