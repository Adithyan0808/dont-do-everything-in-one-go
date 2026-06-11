using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Infrastructure.Repositories;

public sealed class AuditLogRepository(ApplicationDbContext dbContext)
    : GenericRepository<AuditLog>(dbContext), IAuditLogRepository
{
    public async Task<IReadOnlyList<AuditLog>> GetEntityAuditTrailAsync(string entityName, string entityId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(auditLog => auditLog.EntityName == entityName && auditLog.EntityId == entityId)
            .OrderByDescending(auditLog => auditLog.Timestamp)
            .ToListAsync(cancellationToken);
    }
}
