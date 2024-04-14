using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Common.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace ProductManagement.API.Extensions
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            var statusCode = GetStatusCode(exception);
            var errors = GetErrors(exception);

            var details = new ProblemDetails
            {
                Detail = exception.Message,
                Status = statusCode,
                Extensions = errors is null ? null! : errors.ToDictionary(x => x.Key, x => (object?)x.Value),
                Title = "API Exception",
                Type = exception.GetType().Name,
            };

            var response = JsonSerializer.Serialize(details);
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(response, cancellationToken);

            return true;
        }


        private static int GetStatusCode(Exception exception) => exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            CustomValidationException => (int)HttpStatusCode.UnprocessableEntity,
            _ => (int)HttpStatusCode.InternalServerError,
        };
        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null!;
            if (exception is CustomValidationException validationException)
            {
                errors = validationException.Errors;
            }
            return errors;
        }


    }
}
