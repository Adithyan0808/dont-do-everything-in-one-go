using CertificationHub.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CertificationHub.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IUserRepository Users { get; }

    IDriveRepository Drives { get; }

    IRegistrationRepository Registrations { get; }

    IVoucherRepository Vouchers { get; }

    IAuditLogRepository AuditLogs { get; }

    INotificationRepository Notifications { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
