using CertificationHub.Application.Features.Vouchers.Commands;
using FluentValidation;

namespace CertificationHub.Application.Validators;

public sealed class AssignVoucherCommandValidator : AbstractValidator<AssignVoucherCommand>
{
    public AssignVoucherCommandValidator()
    {
        RuleFor(command => command.Voucher.VoucherId).NotEmpty();
        RuleFor(command => command.Voucher.RegistrationId).NotEmpty();
        RuleFor(command => command.Voucher.AssignedToUserId).NotEmpty();
    }
}
