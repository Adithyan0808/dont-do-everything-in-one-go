using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Interfaces;

public interface IDriveRepository : IGenericRepository<CertificationDrive>
{
    Task<IReadOnlyList<CertificationDrive>> GetActiveDrivesAsync(CancellationToken cancellationToken = default);

    Task<DriveDashboardReadModel?> GetDriveDashboardAsync(Guid driveId, CancellationToken cancellationToken = default);
}
