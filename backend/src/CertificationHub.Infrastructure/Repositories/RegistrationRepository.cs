using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using CertificationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Infrastructure.Repositories;

public sealed class RegistrationRepository(ApplicationDbContext dbContext)
    : GenericRepository<Registration>(dbContext), IRegistrationRepository
{
    public async Task<IReadOnlyList<Registration>> GetPendingApprovalsAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(registration => registration.User)
            .Include(registration => registration.Drive)
            .Where(registration => registration.Status == RegistrationStatus.Pending)
            .OrderBy(registration => registration.RegisteredDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Registration>> GetUserRegistrationsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(registration => registration.Drive)
            .Include(registration => registration.EligibilityRecord)
            .Include(registration => registration.AssessmentResult)
            .Where(registration => registration.UserId == userId)
            .OrderByDescending(registration => registration.RegisteredDate)
            .ToListAsync(cancellationToken);
    }
}
