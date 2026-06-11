using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Interfaces;

public interface IVoucherRepository : IGenericRepository<Voucher>
{
    Task<IReadOnlyList<Voucher>> GetAvailableVouchersAsync(Guid driveId, CancellationToken cancellationToken = default);

    Task AssignVoucherAsync(Guid voucherId, Guid userId, CancellationToken cancellationToken = default);
}
