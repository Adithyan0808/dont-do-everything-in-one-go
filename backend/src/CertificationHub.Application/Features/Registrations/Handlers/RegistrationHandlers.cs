using AutoMapper;
using CertificationHub.Application.Exceptions;
using CertificationHub.Application.Features.Registrations.Commands;
using CertificationHub.Application.Features.Registrations.DTOs;
using CertificationHub.Application.Features.Registrations.Queries;
using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using MediatR;

namespace CertificationHub.Application.Features.Registrations.Handlers;

public sealed class RegisterForDriveCommandHandler(
    IDriveRepository driveRepository,
    IRegistrationRepository registrationRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<RegisterForDriveCommand, RegistrationResponseDto>
{
    public async Task<RegistrationResponseDto> Handle(RegisterForDriveCommand request, CancellationToken cancellationToken)
    {
        var drive = await driveRepository.GetByIdAsync(request.Registration.DriveId, cancellationToken)
            ?? throw new NotFoundException(nameof(CertificationDrive), request.Registration.DriveId);

        if (DateTime.UtcNow > drive.RegistrationEndDate)
        {
            throw new BusinessRuleException("Registration window has closed.");
        }

        if (drive.CurrentRegistrationCount >= drive.TargetCount)
        {
            throw new BusinessRuleException("Drive target count has already been reached.");
        }

        if (request.Registration.PriorAttempts >= drive.AttemptLimit)
        {
            throw new BusinessRuleException("Attempt limit exceeded.");
        }

        var existing = await registrationRepository.FindAsync(
            registration => registration.UserId == request.Registration.UserId && registration.DriveId == request.Registration.DriveId,
            cancellationToken);
        if (existing.Any())
        {
            throw new BusinessRuleException("User already has an active registration for this drive.");
        }

        var registrationEntity = new Registration
        {
            RegistrationId = Guid.NewGuid(),
            DriveId = request.Registration.DriveId,
            UserId = request.Registration.UserId,
            ExamTrack = request.Registration.ExamTrack,
            PreferredSlot = request.Registration.PreferredSlot,
            PriorAttempts = request.Registration.PriorAttempts,
            Status = RegistrationStatus.Pending,
            RegisteredDate = DateTime.UtcNow,
            CreatedBy = "application"
        };

        drive.CurrentRegistrationCount++;
        await registrationRepository.AddAsync(registrationEntity, cancellationToken);
        driveRepository.Update(drive);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<RegistrationResponseDto>(registrationEntity);
    }
}

public sealed class RegistrationCommandHandlers(
    IRegistrationRepository registrationRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) :
    IRequestHandler<ApproveRegistrationCommand, RegistrationResponseDto>,
    IRequestHandler<RejectRegistrationCommand, RegistrationResponseDto>,
    IRequestHandler<ScheduleExamCommand, RegistrationResponseDto>,
    IRequestHandler<UpdateRegistrationStatusCommand, RegistrationResponseDto>
{
    public Task<RegistrationResponseDto> Handle(ApproveRegistrationCommand request, CancellationToken cancellationToken)
        => UpdateApprovalAsync(request.RegistrationId, request.ApproverId, request.Comments, RegistrationStatus.Approved, cancellationToken);

    public Task<RegistrationResponseDto> Handle(RejectRegistrationCommand request, CancellationToken cancellationToken)
        => UpdateApprovalAsync(request.RegistrationId, request.ApproverId, request.Comments, RegistrationStatus.Rejected, cancellationToken);

    public async Task<RegistrationResponseDto> Handle(ScheduleExamCommand request, CancellationToken cancellationToken)
    {
        var registration = await GetRegistrationAsync(request.RegistrationId, cancellationToken);
        registration.PreferredSlot = request.PreferredSlot;
        registrationRepository.Update(registration);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<RegistrationResponseDto>(registration);
    }

    public async Task<RegistrationResponseDto> Handle(UpdateRegistrationStatusCommand request, CancellationToken cancellationToken)
    {
        var registration = await GetRegistrationAsync(request.RegistrationId, cancellationToken);
        registration.Status = request.Status;
        registrationRepository.Update(registration);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<RegistrationResponseDto>(registration);
    }

    private async Task<RegistrationResponseDto> UpdateApprovalAsync(Guid registrationId, Guid approverId, string comments, RegistrationStatus status, CancellationToken cancellationToken)
    {
        var registration = await GetRegistrationAsync(registrationId, cancellationToken);
        registration.ApproverId = approverId;
        registration.ApprovalDate = DateTime.UtcNow;
        registration.ApprovalComments = comments;
        registration.Status = status;
        registrationRepository.Update(registration);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<RegistrationResponseDto>(registration);
    }

    private async Task<Registration> GetRegistrationAsync(Guid registrationId, CancellationToken cancellationToken)
        => await registrationRepository.GetByIdAsync(registrationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Registration), registrationId);
}

public sealed class RegistrationQueryHandlers(
    IRegistrationRepository registrationRepository,
    IMapper mapper) :
    IRequestHandler<GetRegistrationByIdQuery, RegistrationResponseDto>,
    IRequestHandler<GetRegistrationsByDriveQuery, IReadOnlyList<RegistrationSummaryDto>>,
    IRequestHandler<GetUserRegistrationsQuery, IReadOnlyList<RegistrationSummaryDto>>
{
    public async Task<RegistrationResponseDto> Handle(GetRegistrationByIdQuery request, CancellationToken cancellationToken)
    {
        var registration = await registrationRepository.GetByIdAsync(request.RegistrationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Registration), request.RegistrationId);
        return mapper.Map<RegistrationResponseDto>(registration);
    }

    public async Task<IReadOnlyList<RegistrationSummaryDto>> Handle(GetRegistrationsByDriveQuery request, CancellationToken cancellationToken)
    {
        var registrations = await registrationRepository.FindAsync(registration => registration.DriveId == request.DriveId, cancellationToken);
        return mapper.Map<IReadOnlyList<RegistrationSummaryDto>>(registrations);
    }

    public async Task<IReadOnlyList<RegistrationSummaryDto>> Handle(GetUserRegistrationsQuery request, CancellationToken cancellationToken)
    {
        var registrations = await registrationRepository.GetUserRegistrationsAsync(request.UserId, cancellationToken);
        return mapper.Map<IReadOnlyList<RegistrationSummaryDto>>(registrations);
    }
}
