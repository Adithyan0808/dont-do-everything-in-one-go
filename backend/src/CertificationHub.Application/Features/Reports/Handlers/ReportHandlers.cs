using CertificationHub.Application.Features.Reports.DTOs;
using CertificationHub.Application.Features.Reports.Queries;
using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using MediatR;

namespace CertificationHub.Application.Features.Reports.Handlers;

public sealed class DashboardStatsQueryHandler(
    IDriveRepository driveRepository,
    IRegistrationRepository registrationRepository,
    IVoucherRepository voucherRepository) : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var activeDrives = await driveRepository.CountAsync(
            drive => drive.Status == DriveStatus.Open || drive.Status == DriveStatus.InProgress,
            cancellationToken);
        var registrations = await registrationRepository.CountAsync(null, cancellationToken);
        var certified = await registrationRepository.CountAsync(registration => registration.Status == RegistrationStatus.Completed, cancellationToken);
        var availableVouchers = await voucherRepository.CountAsync(voucher => voucher.Status == VoucherStatus.Available, cancellationToken);

        return new DashboardStatsDto(activeDrives, registrations, certified, availableVouchers);
    }
}

public sealed class DriveFunnelQueryHandler(
    IDriveRepository driveRepository,
    IRegistrationRepository registrationRepository,
    IVoucherRepository voucherRepository) : IRequestHandler<GetDriveFunnelQuery, DriveFunnelDto>
{
    public async Task<DriveFunnelDto> Handle(GetDriveFunnelQuery request, CancellationToken cancellationToken)
    {
        var drive = await driveRepository.GetByIdAsync(request.DriveId, cancellationToken)
            ?? throw new Exceptions.NotFoundException(nameof(CertificationDrive), request.DriveId);
        var registrations = await registrationRepository.FindAsync(registration => registration.DriveId == request.DriveId, cancellationToken);
        var vouchers = await voucherRepository.FindAsync(voucher => voucher.DriveId == request.DriveId, cancellationToken);

        return new DriveFunnelDto(
            drive.DriveId,
            drive.DriveName,
            registrations.Count,
            registrations.Count(item => item.Status is RegistrationStatus.Approved or RegistrationStatus.Completed),
            vouchers.Count(item => item.AssignedToUserId.HasValue),
            registrations.Count(item => item.Status == RegistrationStatus.Completed));
    }
}

public sealed class UtilizationReportQueryHandler(
    IDriveRepository driveRepository,
    IVoucherRepository voucherRepository) : IRequestHandler<GetUtilizationReportQuery, UtilizationReportDto>
{
    public async Task<UtilizationReportDto> Handle(GetUtilizationReportQuery request, CancellationToken cancellationToken)
    {
        var drive = await driveRepository.GetByIdAsync(request.DriveId, cancellationToken)
            ?? throw new Exceptions.NotFoundException(nameof(CertificationDrive), request.DriveId);
        var voucherCount = await voucherRepository.CountAsync(voucher => voucher.DriveId == request.DriveId, cancellationToken);

        return new UtilizationReportDto(drive.DriveId, drive.DriveName, drive.BudgetAllocated, drive.BudgetConsumed, voucherCount);
    }
}

public sealed class CertificationTrendQueryHandler(
    IGenericRepository<EmployeeCertification> certificationRepository) : IRequestHandler<GetCertificationTrendQuery, IReadOnlyList<CertificationTrendDto>>
{
    public async Task<IReadOnlyList<CertificationTrendDto>> Handle(GetCertificationTrendQuery request, CancellationToken cancellationToken)
    {
        var certifications = await certificationRepository.GetAllAsync(cancellationToken);
        return certifications
            .GroupBy(certification => new { certification.CertificationName, certification.IssuedDate.Year, certification.IssuedDate.Month })
            .Select(group => new CertificationTrendDto(group.Key.CertificationName, group.Count(), group.Key.Year, group.Key.Month))
            .OrderBy(item => item.Year)
            .ThenBy(item => item.Month)
            .ToArray();
    }
}
