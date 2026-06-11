using CertificationHub.Application.Features.Assessments.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Assessments.Commands;

public sealed record SubmitAssessmentCommand(SubmitAssessmentDto Assessment) : IRequest<AssessmentResponseDto>;
