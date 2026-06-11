using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Interfaces;

public interface IAuditLogRepository : IGenericRepository<AuditLog>
{
    Task<IReadOnlyList<AuditLog>> GetEntityAuditTrailAsync(string entityName, string entityId, CancellationToken cancellationToken = default);
}
