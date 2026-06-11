using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Features.Registrations.DTOs;

public sealed record CreateRegistrationDto(Guid DriveId, Guid UserId, string ExamTrack, string PreferredSlot, int PriorAttempts);

public sealed record ApprovalRequestDto(Guid RegistrationId, Guid ApproverId, string Comments);

public sealed record RegistrationResponseDto(
    Guid RegistrationId,
    Guid DriveId,
    Guid UserId,
    string ExamTrack,
    string PreferredSlot,
    int PriorAttempts,
    RegistrationStatus Status,
    DateTime RegisteredDate,
    DateTime? ApprovalDate,
    string ApprovalComments);

public sealed record RegistrationSummaryDto(
    Guid RegistrationId,
    string DriveName,
    string CandidateName,
    RegistrationStatus Status,
    DateTime RegisteredDate);
