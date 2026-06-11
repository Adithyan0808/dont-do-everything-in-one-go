using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using CertificationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Infrastructure.Repositories;

public sealed class DriveRepository(ApplicationDbContext dbContext)
    : GenericRepository<CertificationDrive>(dbContext), IDriveRepository
{
    public async Task<IReadOnlyList<CertificationDrive>> GetActiveDrivesAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(drive => drive.Status == DriveStatus.Open || drive.Status == DriveStatus.InProgress)
            .OrderBy(drive => drive.RegistrationStartDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<DriveDashboardReadModel?> GetDriveDashboardAsync(Guid driveId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(drive => drive.DriveId == driveId)
            .Select(drive => new DriveDashboardReadModel(
                drive.DriveId,
                drive.DriveName,
                drive.TargetCount,
                drive.CurrentRegistrationCount,
                drive.CurrentCertifiedCount,
                drive.BudgetAllocated,
                drive.BudgetConsumed))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
