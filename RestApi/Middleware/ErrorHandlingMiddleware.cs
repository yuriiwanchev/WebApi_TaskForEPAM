using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using Domain.Helpers;
using Microsoft.AspNetCore.Http;

namespace RestApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch(error)
                {
                    case InvalidUserInputException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case AlreadyExistenceException e:
                        response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
                // await Results.Text(ex.Message).ExecuteAsync(httpContext);
            }
        }
    }
}