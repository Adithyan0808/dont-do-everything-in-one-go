using CertificationHub.Application.Features.Vouchers.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Vouchers.Commands;

public sealed record CreateVoucherPoolCommand(CreateVoucherPoolDto VoucherPool) : IRequest<IReadOnlyList<VoucherResponseDto>>;

public sealed record AssignVoucherCommand(AssignVoucherDto Voucher) : IRequest<VoucherResponseDto>;

public sealed record DeliverVoucherCommand(Guid VoucherId) : IRequest<VoucherResponseDto>;

public sealed record RevokeVoucherCommand(Guid VoucherId) : IRequest<VoucherResponseDto>;

public sealed record ReissueVoucherCommand(Guid VoucherId) : IRequest<VoucherResponseDto>;
