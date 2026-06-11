using CertificationHub.Application.Interfaces;
using CertificationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace CertificationHub.Infrastructure.UnitOfWork;

public sealed class UnitOfWork(
    ApplicationDbContext dbContext,
    IUserRepository users,
    IDriveRepository drives,
    IRegistrationRepository registrations,
    IVoucherRepository vouchers,
    IAuditLogRepository auditLogs,
    INotificationRepository notifications) : IUnitOfWork, IApplicationUnitOfWork
{
    public IUserRepository Users => users;

    public IDriveRepository Drives => drives;

    public IRegistrationRepository Registrations => registrations;

    public IVoucherRepository Vouchers => vouchers;

    public IAuditLogRepository AuditLogs => auditLogs;

    public INotificationRepository Notifications => notifications;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return dbContext.DisposeAsync();
    }
}
