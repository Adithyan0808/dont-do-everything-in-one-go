using CertificationHub.Application.Features.Vouchers.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Vouchers.Queries;

public sealed record GetVoucherByIdQuery(Guid VoucherId) : IRequest<VoucherResponseDto>;

public sealed record GetAvailableVouchersQuery(Guid DriveId) : IRequest<IReadOnlyList<VoucherResponseDto>>;

public sealed record GetDriveVouchersQuery(Guid DriveId) : IRequest<IReadOnlyList<VoucherResponseDto>>;
