using FluentValidation;
using System.Net;
using System.Text.Json;
using TechTask.AA.API.DTO;
using TechTask.AA.Core.Exceptions;

namespace TechTask.AA.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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
            var exceptionType = exception.GetType();

            var response = new ResponseDTO
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace
            };

            if (exceptionType == typeof(BadRequestException) || exceptionType == typeof(ValidationException))
            {
                response.Status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                response.Status = HttpStatusCode.NotFound;
            }
            else
            {
                response.Status = HttpStatusCode.InternalServerError;
            }

            var exceptionResult = JsonSerializer.Serialize(new { Error = response.Message, response.StackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.Status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
