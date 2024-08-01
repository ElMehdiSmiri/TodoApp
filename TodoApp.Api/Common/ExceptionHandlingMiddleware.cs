using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TodoApp.Application.Common.Exceptions;

namespace TodoApp.Api.Common
{

    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            object result;
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    result = new ValidationProblemDetails(validationEx.Errors)
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        Status = StatusCodes.Status400BadRequest,
                    };
                    break;

                case NotFoundException notFoundEx:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    result = new ValidationProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Status = StatusCodes.Status404NotFound,
                        Title = "The specified resource was not found.",
                        Detail = notFoundEx.Message
                    };
                    break;

                case ForbiddenAccessException:
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    result = new ValidationProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                        Status = StatusCodes.Status403Forbidden,
                        Title = "Forbidden",
                        Detail = "You do not have permission to access this resource."
                    };
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    result = new ValidationProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "An error occurred while processing your request.",
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                        Detail = exception.ToString()
                    };
                    break;
            }

            string resultJson = JsonSerializer.Serialize(result);
            return context.Response.WriteAsync(resultJson);
        }
    }
}
