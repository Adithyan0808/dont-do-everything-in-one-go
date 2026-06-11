using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Services;

public interface IAuditService
{
    Task LogActionAsync(
        string entityName,
        string entityId,
        string action,
        Guid actorId,
        string actorName,
        string beforeSnapshotJson,
        string afterSnapshotJson,
        Guid? driveId = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AuditLog>> GetAuditTrailAsync(string entityName, string entityId, CancellationToken cancellationToken = default);
}
