using System;
using System.Net;
using System.Threading.Tasks;
using Common;
using Elevator.Api.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Elevator.Api.Middlewares
{
    public class HandleExceptionsMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ApiException apiException)
            {
                HttpVoidOperationResult error;
                switch (apiException.StatusCode)
                {
                    case HttpStatusCode.InternalServerError:
                        error = HttpVoidOperationResult.InternalServerError(apiException.Message);
                        await WriteErrorAsync(context, error);
                        break;
                    case HttpStatusCode.Forbidden:
                        error = HttpVoidOperationResult.Forbidden(apiException.Message);
                        await WriteErrorAsync(context, error);
                        break;
                    case HttpStatusCode.NotFound:
                        error = HttpVoidOperationResult.NotFound(apiException.Message);
                        await WriteErrorAsync(context, error);
                        break;
                }
            }
            catch (Exception e)
            {
                var error = HttpVoidOperationResult.InternalServerError(e.Message);
                await WriteErrorAsync(context, error);
            }
        }

        private static Task WriteErrorAsync(HttpContext httpContext, HttpVoidOperationResult result)
        {
            httpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
            httpContext.Response.Headers[HeaderNames.Pragma] = "no-cache";
            httpContext.Response.Headers[HeaderNames.Expires] = "-1";
            httpContext.Response.Headers.Remove(HeaderNames.ETag);

            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            httpContext.Response.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(result);

            return httpContext.Response.WriteAsync(json);
        }

    }
}
