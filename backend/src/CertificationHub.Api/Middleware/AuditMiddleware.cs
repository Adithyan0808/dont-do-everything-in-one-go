using System.Diagnostics;

namespace CertificationHub.Api.Middleware;

public sealed class AuditMiddleware(RequestDelegate next, ILogger<AuditMiddleware> logger)
{
    private static readonly string[] SensitiveOperations =
    [
        "/vouchers/assign",
        "/vouchers/revoke",
        "/registrations/",
        "/drives"
    ];

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await next(context);
        stopwatch.Stop();

        var path = context.Request.Path.Value ?? string.Empty;
        var isSensitive = SensitiveOperations.Any(operation => path.Contains(operation, StringComparison.OrdinalIgnoreCase));

        logger.LogInformation(
            "HTTP audit {Method} {Path} user {User} status {StatusCode} elapsed {ElapsedMilliseconds}ms sensitive {Sensitive}",
            context.Request.Method,
            path,
            context.User.Identity?.Name ?? "anonymous",
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds,
            isSensitive);
    }
}
