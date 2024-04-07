using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

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

            var details = new ProblemDetails
            {
                Detail = "Thrown " + exception.Message,
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "API Exception",
                Type = "Server error"
            };
            var response = JsonSerializer.Serialize(details);

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(response, cancellationToken);

            return true;
        }
    }
}
