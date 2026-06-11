using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Features.Vouchers.DTOs;

public sealed record CreateVoucherPoolDto(Guid DriveId, string Vendor, IReadOnlyList<string> VoucherCodes, decimal Value);

public sealed record AssignVoucherDto(Guid VoucherId, Guid RegistrationId, Guid AssignedToUserId);

public sealed record VoucherResponseDto(
    Guid VoucherId,
    Guid DriveId,
    string Vendor,
    string MaskedCode,
    decimal Value,
    VoucherStatus Status,
    Guid? AssignedToUserId,
    DateTime? AssignedDate,
    DateTime? DeliveryDate,
    DateTime? RedeemedDate,
    string TokenizedLink);

public sealed record VoucherDashboardDto(Guid DriveId, int Available, int Assigned, int Delivered, int Redeemed, decimal TotalValue);
