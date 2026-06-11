using System.Net;
using CertificationHub.Api.Models;
using CertificationHub.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Api.Middleware;

public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var correlationId = context.Items["CorrelationId"]?.ToString() ?? context.TraceIdentifier;
        var (statusCode, message, errors) = exception switch
        {
            ValidationException validationException => (HttpStatusCode.BadRequest, "Validation failed.", validationException.Errors),
            BusinessRuleException businessRuleException => (HttpStatusCode.BadRequest, businessRuleException.Message, new[] { businessRuleException.Message }),
            NotFoundException notFoundException => (HttpStatusCode.NotFound, notFoundException.Message, new[] { notFoundException.Message }),
            CertificationHub.Application.Exceptions.UnauthorizedException unauthorizedException => (HttpStatusCode.Unauthorized, unauthorizedException.Message, new[] { unauthorizedException.Message }),
            UnauthorizedAccessException unauthorizedAccessException => (HttpStatusCode.Forbidden, unauthorizedAccessException.Message, new[] { unauthorizedAccessException.Message }),
            DbUpdateConcurrencyException concurrencyException => (HttpStatusCode.Conflict, "A concurrency conflict occurred.", new[] { concurrencyException.Message }),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.", new[] { "Internal server error." })
        };

        logger.LogError(exception, "Unhandled exception. CorrelationId: {CorrelationId}", correlationId);
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(ApiResponse.Fail(message, errors, correlationId));
    }
}
