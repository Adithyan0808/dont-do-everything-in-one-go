using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Services;

public sealed class AuditService(
    IAuditLogRepository auditLogRepository,
    IApplicationUnitOfWork unitOfWork) : IAuditService
{
    public async Task LogActionAsync(
        string entityName,
        string entityId,
        string action,
        Guid actorId,
        string actorName,
        string beforeSnapshotJson,
        string afterSnapshotJson,
        Guid? driveId = null,
        CancellationToken cancellationToken = default)
    {
        await auditLogRepository.AddAsync(new AuditLog
        {
            AuditLogId = Guid.NewGuid(),
            DriveId = driveId,
            EntityName = entityName,
            EntityId = entityId,
            Action = action,
            ActorId = actorId,
            ActorName = actorName,
            BeforeSnapshotJson = beforeSnapshotJson,
            AfterSnapshotJson = afterSnapshotJson,
            Timestamp = DateTime.UtcNow,
            CreatedBy = "application"
        }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<IReadOnlyList<AuditLog>> GetAuditTrailAsync(string entityName, string entityId, CancellationToken cancellationToken = default)
        => auditLogRepository.GetEntityAuditTrailAsync(entityName, entityId, cancellationToken);
}
