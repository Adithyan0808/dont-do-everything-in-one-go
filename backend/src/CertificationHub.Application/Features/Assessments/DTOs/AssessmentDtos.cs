using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Features.Assessments.DTOs;

public sealed record SubmitAssessmentDto(Guid RegistrationId, int Score, int MaxScore, string EvidenceUrl, string SubmittedBy);

public sealed record AssessmentResponseDto(
    Guid ResultId,
    Guid RegistrationId,
    int Score,
    int MaxScore,
    AssessmentOutcome Outcome,
    string EvidenceUrl,
    string SubmittedBy,
    DateTime SubmissionDate);

public sealed record AssessmentSummaryDto(Guid ResultId, string CandidateName, string DriveName, int Score, AssessmentOutcome Outcome);
