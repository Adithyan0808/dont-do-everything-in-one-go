using CertificationHub.Application.Features.Drives.Commands;
using FluentValidation;

namespace CertificationHub.Application.Validators;

public sealed class CreateDriveCommandValidator : AbstractValidator<CreateDriveCommand>
{
    public CreateDriveCommandValidator()
    {
        RuleFor(command => command.Drive.DriveName).NotEmpty().MaximumLength(250);
        RuleFor(command => command.Drive.VendorName).NotEmpty().MaximumLength(150);
        RuleFor(command => command.Drive.CertificationName).NotEmpty().MaximumLength(250);
        RuleFor(command => command.Drive.BudgetAllocated).GreaterThan(0);
        RuleFor(command => command.Drive.AttemptLimit).GreaterThan(0);
        RuleFor(command => command.Drive.TenureRequiredDays).GreaterThanOrEqualTo(0);
        RuleFor(command => command.Drive.TargetCount).GreaterThan(0);
        RuleFor(command => command.Drive.RegistrationEndDate)
            .GreaterThan(command => command.Drive.RegistrationStartDate)
            .WithMessage("Registration end date must be after registration start date.");
        RuleFor(command => command.Drive.ExamWindowStartDate)
            .GreaterThanOrEqualTo(command => command.Drive.RegistrationEndDate)
            .WithMessage("Exam window must start after registration closes.");
        RuleFor(command => command.Drive.ExamWindowEndDate)
            .GreaterThan(command => command.Drive.ExamWindowStartDate)
            .WithMessage("Exam window end date must be after exam window start date.");
    }
}
