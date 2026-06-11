using CertificationHub.Application.Features.Registrations.Commands;
using FluentValidation;

namespace CertificationHub.Application.Validators;

public sealed class RegisterForDriveCommandValidator : AbstractValidator<RegisterForDriveCommand>
{
    public RegisterForDriveCommandValidator()
    {
        RuleFor(command => command.Registration.DriveId).NotEmpty();
        RuleFor(command => command.Registration.UserId).NotEmpty();
        RuleFor(command => command.Registration.ExamTrack).NotEmpty().MaximumLength(150);
        RuleFor(command => command.Registration.PreferredSlot).MaximumLength(150);
        RuleFor(command => command.Registration.PriorAttempts).GreaterThanOrEqualTo(0);
    }
}
