using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using UserManagement.Application.Common.CustomExceptions;

namespace UserManagement.API.Extensions
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            var statusCode = GetStatusCode(exception);

            var details = new ProblemDetails
            {
                Detail = exception.Message,
                Status = statusCode,
                Title = "API Exception",
                Type = exception.GetType().ToString(),
            };
            var response = JsonSerializer.Serialize(details);
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(response, cancellationToken);

            return true;
        }

        private int GetStatusCode(Exception exception) => exception switch
        {
            BadRequestException => (int)HttpStatusCode.BadRequest,
            NotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError,
        };


    }
}
