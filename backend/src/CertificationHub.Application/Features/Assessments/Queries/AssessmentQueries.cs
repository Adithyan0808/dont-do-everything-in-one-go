using CertificationHub.Application.Features.Assessments.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Assessments.Queries;

public sealed record GetAssessmentByIdQuery(Guid ResultId) : IRequest<AssessmentResponseDto>;

public sealed record GetAssessmentsByDriveQuery(Guid DriveId) : IRequest<IReadOnlyList<AssessmentSummaryDto>>;
