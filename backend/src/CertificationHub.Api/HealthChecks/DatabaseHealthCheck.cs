using CertificationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CertificationHub.Api.HealthChecks;

public sealed class DatabaseHealthCheck(ApplicationDbContext dbContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);
            if (!canConnect)
            {
                return HealthCheckResult.Unhealthy("Database connectivity check failed.");
            }

            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
            return pendingMigrations.Any()
                ? HealthCheckResult.Degraded("Database has pending migrations.", data: new Dictionary<string, object> { ["pendingMigrations"] = pendingMigrations.ToArray() })
                : HealthCheckResult.Healthy("Database is reachable and migrations are current.");
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy("Database health check failed.", exception);
        }
    }
}
