using CertificationHub.Application.Features.Assessments.Commands;
using FluentValidation;

namespace CertificationHub.Application.Validators;

public sealed class SubmitAssessmentCommandValidator : AbstractValidator<SubmitAssessmentCommand>
{
    public SubmitAssessmentCommandValidator()
    {
        RuleFor(command => command.Assessment.RegistrationId).NotEmpty();
        RuleFor(command => command.Assessment.Score).GreaterThanOrEqualTo(0);
        RuleFor(command => command.Assessment.MaxScore).GreaterThan(0);
        RuleFor(command => command.Assessment.Score)
            .LessThanOrEqualTo(command => command.Assessment.MaxScore)
            .WithMessage("Score cannot exceed max score.");
        RuleFor(command => command.Assessment.EvidenceUrl).MaximumLength(1000);
        RuleFor(command => command.Assessment.SubmittedBy).NotEmpty().MaximumLength(256);
    }
}
