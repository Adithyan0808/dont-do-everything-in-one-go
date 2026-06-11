using AutoMapper;
using CertificationHub.Application.DTOs;
using CertificationHub.Application.Exceptions;
using CertificationHub.Application.Features.Assessments.Commands;
using CertificationHub.Application.Features.Assessments.DTOs;
using CertificationHub.Application.Features.Assessments.Queries;
using CertificationHub.Application.Interfaces;
using CertificationHub.Application.Services;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace CertificationHub.Application.Features.Assessments.Handlers;

public sealed class SubmitAssessmentCommandHandler(
    IGenericRepository<AssessmentResult> assessmentRepository,
    IRegistrationRepository registrationRepository,
    IApplicationUnitOfWork unitOfWork,
    IOptions<AssessmentSettings> settings,
    INotificationService notificationService,
    IMapper mapper) : IRequestHandler<SubmitAssessmentCommand, AssessmentResponseDto>
{
    public async Task<AssessmentResponseDto> Handle(SubmitAssessmentCommand request, CancellationToken cancellationToken)
    {
        var registration = await registrationRepository.GetByIdAsync(request.Assessment.RegistrationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Registration), request.Assessment.RegistrationId);

        var drive = registration.Drive ?? throw new BusinessRuleException("Registration drive details are required.");
        var passingScore = settings.Value.PassMarks
            .FirstOrDefault(mark =>
                mark.Vendor.Equals(drive.VendorName, StringComparison.OrdinalIgnoreCase) &&
                mark.Certification.Equals(drive.CertificationName, StringComparison.OrdinalIgnoreCase))
            ?.PassingScore ?? 700;

        var outcome = request.Assessment.Score >= passingScore ? AssessmentOutcome.Passed : AssessmentOutcome.Failed;
        var result = new AssessmentResult
        {
            ResultId = Guid.NewGuid(),
            RegistrationId = request.Assessment.RegistrationId,
            Score = request.Assessment.Score,
            MaxScore = request.Assessment.MaxScore,
            Outcome = outcome,
            EvidenceUrl = request.Assessment.EvidenceUrl,
            SubmittedBy = request.Assessment.SubmittedBy,
            SubmissionDate = DateTime.UtcNow,
            CreatedBy = "application"
        };

        registration.Status = outcome == AssessmentOutcome.Passed ? RegistrationStatus.Completed : registration.Status;
        await assessmentRepository.AddAsync(result, cancellationToken);
        registrationRepository.Update(registration);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await notificationService.SendAssessmentNotificationAsync(registration.UserId, outcome.ToString(), cancellationToken);

        return mapper.Map<AssessmentResponseDto>(result);
    }
}

public sealed class AssessmentQueryHandlers(
    IGenericRepository<AssessmentResult> assessmentRepository,
    IMapper mapper) :
    IRequestHandler<GetAssessmentByIdQuery, AssessmentResponseDto>,
    IRequestHandler<GetAssessmentsByDriveQuery, IReadOnlyList<AssessmentSummaryDto>>
{
    public async Task<AssessmentResponseDto> Handle(GetAssessmentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await assessmentRepository.GetByIdAsync(request.ResultId, cancellationToken)
            ?? throw new NotFoundException(nameof(AssessmentResult), request.ResultId);
        return mapper.Map<AssessmentResponseDto>(result);
    }

    public async Task<IReadOnlyList<AssessmentSummaryDto>> Handle(GetAssessmentsByDriveQuery request, CancellationToken cancellationToken)
    {
        var results = await assessmentRepository.FindAsync(
            result => result.Registration != null && result.Registration.DriveId == request.DriveId,
            cancellationToken);
        return mapper.Map<IReadOnlyList<AssessmentSummaryDto>>(results);
    }
}
