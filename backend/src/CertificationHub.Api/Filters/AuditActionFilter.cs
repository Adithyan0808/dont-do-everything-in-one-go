using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Context;

namespace CertificationHub.Api.Filters;

public sealed class AuditActionFilter(ILogger<AuditActionFilter> logger) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        using (LogContext.PushProperty("Controller", context.RouteData.Values["controller"]))
        using (LogContext.PushProperty("Action", context.RouteData.Values["action"]))
        {
            logger.LogInformation("Executing API action {Controller}.{Action}", context.RouteData.Values["controller"], context.RouteData.Values["action"]);
            await next();
        }
    }
}
