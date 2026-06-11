using AutoMapper;
using CertificationHub.Application.Exceptions;
using CertificationHub.Application.Features.Drives.Commands;
using CertificationHub.Application.Features.Drives.DTOs;
using CertificationHub.Application.Features.Drives.Queries;
using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using MediatR;

namespace CertificationHub.Application.Features.Drives.Handlers;

public sealed class CreateDriveCommandHandler(
    IDriveRepository driveRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateDriveCommand, DriveResponseDto>
{
    public async Task<DriveResponseDto> Handle(CreateDriveCommand request, CancellationToken cancellationToken)
    {
        ValidateDriveWindow(request.Drive.RegistrationStartDate, request.Drive.RegistrationEndDate, request.Drive.ExamWindowStartDate, request.Drive.ExamWindowEndDate);
        ValidateBudget(request.Drive.BudgetAllocated);

        var activeDrives = await driveRepository.GetActiveDrivesAsync(cancellationToken);
        if (activeDrives.Any(drive =>
            drive.VendorName == request.Drive.VendorName &&
            drive.CertificationName == request.Drive.CertificationName &&
            request.Drive.RegistrationStartDate <= drive.RegistrationEndDate &&
            request.Drive.RegistrationEndDate >= drive.RegistrationStartDate))
        {
            throw new BusinessRuleException("An overlapping active drive already exists for this certification.");
        }

        var drive = mapper.Map<CertificationDrive>(request.Drive);
        drive.DriveId = Guid.NewGuid();
        drive.DriveCode = GenerateDriveCode(drive.VendorName, drive.CertificationName, drive.RegistrationStartDate);
        drive.Status = DriveStatus.Draft;

        await driveRepository.AddAsync(drive, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<DriveResponseDto>(drive);
    }

    private static string GenerateDriveCode(string vendor, string certification, DateTime startDate)
    {
        var vendorCode = new string(vendor.Where(char.IsLetterOrDigit).Take(4).ToArray()).ToUpperInvariant();
        var certCode = new string(certification.Where(char.IsLetterOrDigit).Take(6).ToArray()).ToUpperInvariant();
        return $"{vendorCode}-{certCode}-{startDate:yyyyMM}-{Guid.NewGuid().ToString("N")[..6].ToUpperInvariant()}";
    }

    private static void ValidateDriveWindow(DateTime registrationStart, DateTime registrationEnd, DateTime examStart, DateTime examEnd)
    {
        if (registrationEnd <= registrationStart)
        {
            throw new BusinessRuleException("Registration end date must be after registration start date.");
        }

        if (examEnd <= examStart || examStart < registrationEnd)
        {
            throw new BusinessRuleException("Exam window must start after registration closes and end after it starts.");
        }
    }

    private static void ValidateBudget(decimal budget)
    {
        if (budget <= 0)
        {
            throw new BusinessRuleException("Budget allocation must be greater than zero.");
        }
    }
}

public sealed class UpdateDriveCommandHandler(
    IDriveRepository driveRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateDriveCommand, DriveResponseDto>
{
    public async Task<DriveResponseDto> Handle(UpdateDriveCommand request, CancellationToken cancellationToken)
    {
        var drive = await driveRepository.GetByIdAsync(request.Drive.DriveId, cancellationToken)
            ?? throw new NotFoundException(nameof(CertificationDrive), request.Drive.DriveId);

        drive.DriveName = request.Drive.DriveName;
        drive.SponsorName = request.Drive.SponsorName;
        drive.BudgetAllocated = request.Drive.BudgetAllocated;
        drive.ManagerApprovalRequired = request.Drive.ManagerApprovalRequired;
        drive.AttemptLimit = request.Drive.AttemptLimit;
        drive.TenureRequiredDays = request.Drive.TenureRequiredDays;
        drive.RegistrationStartDate = request.Drive.RegistrationStartDate;
        drive.RegistrationEndDate = request.Drive.RegistrationEndDate;
        drive.ExamWindowStartDate = request.Drive.ExamWindowStartDate;
        drive.ExamWindowEndDate = request.Drive.ExamWindowEndDate;
        drive.TargetCount = request.Drive.TargetCount;
        drive.PolicyUrl = request.Drive.PolicyUrl;

        driveRepository.Update(drive);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<DriveResponseDto>(drive);
    }
}

public sealed class DriveStatusCommandHandler(
    IDriveRepository driveRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) :
    IRequestHandler<ActivateDriveCommand, DriveResponseDto>,
    IRequestHandler<CloseDriveCommand, DriveResponseDto>,
    IRequestHandler<ArchiveDriveCommand, DriveResponseDto>
{
    public Task<DriveResponseDto> Handle(ActivateDriveCommand request, CancellationToken cancellationToken)
        => UpdateStatusAsync(request.DriveId, DriveStatus.Open, cancellationToken);

    public Task<DriveResponseDto> Handle(CloseDriveCommand request, CancellationToken cancellationToken)
        => UpdateStatusAsync(request.DriveId, DriveStatus.Closed, cancellationToken);

    public Task<DriveResponseDto> Handle(ArchiveDriveCommand request, CancellationToken cancellationToken)
        => UpdateStatusAsync(request.DriveId, DriveStatus.Cancelled, cancellationToken);

    private async Task<DriveResponseDto> UpdateStatusAsync(Guid driveId, DriveStatus status, CancellationToken cancellationToken)
    {
        var drive = await driveRepository.GetByIdAsync(driveId, cancellationToken)
            ?? throw new NotFoundException(nameof(CertificationDrive), driveId);

        drive.Status = status;
        driveRepository.Update(drive);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<DriveResponseDto>(drive);
    }
}

public sealed class DriveQueryHandlers(
    IDriveRepository driveRepository,
    IMapper mapper) :
    IRequestHandler<GetDriveByIdQuery, DriveResponseDto>,
    IRequestHandler<GetAllDrivesQuery, IReadOnlyList<DriveListDto>>,
    IRequestHandler<GetDriveDashboardQuery, DriveDashboardDto>
{
    public async Task<DriveResponseDto> Handle(GetDriveByIdQuery request, CancellationToken cancellationToken)
    {
        var drive = await driveRepository.GetByIdAsync(request.DriveId, cancellationToken)
            ?? throw new NotFoundException(nameof(CertificationDrive), request.DriveId);
        return mapper.Map<DriveResponseDto>(drive);
    }

    public async Task<IReadOnlyList<DriveListDto>> Handle(GetAllDrivesQuery request, CancellationToken cancellationToken)
    {
        var drives = request.ActiveOnly
            ? await driveRepository.GetActiveDrivesAsync(cancellationToken)
            : await driveRepository.GetAllAsync(cancellationToken);

        return mapper.Map<IReadOnlyList<DriveListDto>>(drives);
    }

    public async Task<DriveDashboardDto> Handle(GetDriveDashboardQuery request, CancellationToken cancellationToken)
    {
        var dashboard = await driveRepository.GetDriveDashboardAsync(request.DriveId, cancellationToken)
            ?? throw new NotFoundException(nameof(CertificationDrive), request.DriveId);

        var certificationRate = dashboard.CurrentRegistrationCount == 0
            ? 0
            : decimal.Round((decimal)dashboard.CurrentCertifiedCount / dashboard.CurrentRegistrationCount * 100, 2);

        return new DriveDashboardDto(
            dashboard.DriveId,
            dashboard.DriveName,
            dashboard.TargetCount,
            dashboard.CurrentRegistrationCount,
            dashboard.CurrentCertifiedCount,
            dashboard.BudgetAllocated,
            dashboard.BudgetConsumed,
            certificationRate);
    }
}
