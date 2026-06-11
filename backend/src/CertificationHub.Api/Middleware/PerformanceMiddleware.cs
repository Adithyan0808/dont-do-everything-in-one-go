using System.Diagnostics;

namespace CertificationHub.Api.Middleware;

public sealed class PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
{
    private const long SlowRequestThresholdMs = 2000;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await next(context);
        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds > SlowRequestThresholdMs)
        {
            logger.LogWarning(
                "Slow API request {Method} {Path} by {User} took {ElapsedMilliseconds}ms",
                context.Request.Method,
                context.Request.Path,
                context.User.Identity?.Name ?? "anonymous",
                stopwatch.ElapsedMilliseconds);
        }
    }
}
