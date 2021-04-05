using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Elevator.Api.Exceptions;
using Elevator.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
                switch (apiException.StatusCode)
                {
                    case HttpStatusCode.InternalServerError:
                        var error = OperationResult<Unit>.InternalServerError(apiException.Message);
                        await WriteErrorAsync(context, error);
                        break;
                    //todo(likvidator): сделать без свитча 
                }
            }
            catch (Exception e)
            {
                var error = OperationResult<Unit>.InternalServerError(e.Message);
                await WriteErrorAsync(context, error);
            }
        }

        private static Task WriteErrorAsync(HttpContext httpContext, OperationResult<Unit> result)
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
