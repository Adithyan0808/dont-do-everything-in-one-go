using CertificationHub.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CertificationHub.Api.Filters;

public sealed class ValidateModelFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState
            .SelectMany(item => item.Value?.Errors.Select(error => error.ErrorMessage) ?? [])
            .ToArray();
        var correlationId = context.HttpContext.Items["CorrelationId"]?.ToString() ?? context.HttpContext.TraceIdentifier;

        context.Result = new BadRequestObjectResult(ApiResponse.Fail("Validation failed.", errors, correlationId));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
