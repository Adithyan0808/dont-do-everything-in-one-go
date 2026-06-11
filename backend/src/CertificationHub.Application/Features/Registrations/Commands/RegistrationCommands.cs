using CertificationHub.Application.Features.Registrations.DTOs;
using CertificationHub.Domain.Enums;
using MediatR;

namespace CertificationHub.Application.Features.Registrations.Commands;

public sealed record RegisterForDriveCommand(CreateRegistrationDto Registration) : IRequest<RegistrationResponseDto>;

public sealed record ApproveRegistrationCommand(Guid RegistrationId, Guid ApproverId, string Comments) : IRequest<RegistrationResponseDto>;

public sealed record RejectRegistrationCommand(Guid RegistrationId, Guid ApproverId, string Comments) : IRequest<RegistrationResponseDto>;

public sealed record ScheduleExamCommand(Guid RegistrationId, string PreferredSlot) : IRequest<RegistrationResponseDto>;

public sealed record UpdateRegistrationStatusCommand(Guid RegistrationId, RegistrationStatus Status) : IRequest<RegistrationResponseDto>;
