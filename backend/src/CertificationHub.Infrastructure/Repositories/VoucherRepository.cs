using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using CertificationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Infrastructure.Repositories;

public sealed class VoucherRepository(ApplicationDbContext dbContext)
    : GenericRepository<Voucher>(dbContext), IVoucherRepository
{
    public async Task<IReadOnlyList<Voucher>> GetAvailableVouchersAsync(Guid driveId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(voucher => voucher.DriveId == driveId && voucher.Status == VoucherStatus.Available)
            .OrderBy(voucher => voucher.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AssignVoucherAsync(Guid voucherId, Guid userId, CancellationToken cancellationToken = default)
    {
        var voucher = await DbSet.FirstOrDefaultAsync(item => item.VoucherId == voucherId, cancellationToken);

        if (voucher is null)
        {
            throw new InvalidOperationException("Voucher was not found.");
        }

        voucher.AssignedToUserId = userId;
        voucher.AssignedDate = DateTime.UtcNow;
        voucher.Status = VoucherStatus.Assigned;
    }
}
