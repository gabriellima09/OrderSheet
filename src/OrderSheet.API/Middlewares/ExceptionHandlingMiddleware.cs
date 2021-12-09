using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderSheet.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using ValidationException = OrderSheet.Application.Exceptions.ValidationException;

namespace OrderSheet.API.Middlewares
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new ErrorMessageResponse
            {
                Status = statusCode,
                Detail = exception.Message,
                Errors = GetErrors(exception)
            };

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private static int GetStatusCode(Exception ex)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            
            if (ex is ValidationException
                || ex is ArgumentException
                || ex is ArgumentNullException
                || ex is ArgumentOutOfRangeException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }
            else if (ex is AuthenticationException)
                statusCode = StatusCodes.Status401Unauthorized;
            else if (ex is UnauthorizedAccessException)
                statusCode = StatusCodes.Status403Forbidden;

            return statusCode;
        }

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors;
            }

            return errors;
        }
    }
}
