using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using CertificationHub.Application.Exceptions;
using CertificationHub.Application.Features.Vouchers.Commands;
using CertificationHub.Application.Features.Vouchers.DTOs;
using CertificationHub.Application.Features.Vouchers.Queries;
using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using MediatR;

namespace CertificationHub.Application.Features.Vouchers.Handlers;

public sealed class CreateVoucherPoolCommandHandler(
    IVoucherRepository voucherRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateVoucherPoolCommand, IReadOnlyList<VoucherResponseDto>>
{
    public async Task<IReadOnlyList<VoucherResponseDto>> Handle(CreateVoucherPoolCommand request, CancellationToken cancellationToken)
    {
        var vouchers = request.VoucherPool.VoucherCodes.Select(code => new Voucher
        {
            VoucherId = Guid.NewGuid(),
            DriveId = request.VoucherPool.DriveId,
            Vendor = request.VoucherPool.Vendor,
            EncryptedCode = Hash(code),
            MaskedCode = Mask(code),
            Value = request.VoucherPool.Value,
            Status = VoucherStatus.Available,
            CreatedBy = "application"
        }).ToArray();

        await voucherRepository.AddRangeAsync(vouchers, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<IReadOnlyList<VoucherResponseDto>>(vouchers);
    }

    private static string Hash(string value)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));

    private static string Mask(string value)
        => value.Length <= 6 ? "******" : $"{value[..4]}****{value[^2..]}";
}

public sealed class AssignVoucherCommandHandler(
    IVoucherRepository voucherRepository,
    IRegistrationRepository registrationRepository,
    IGenericRepository<EligibilityRecord> eligibilityRepository,
    IGenericRepository<AssessmentResult> assessmentRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<AssignVoucherCommand, VoucherResponseDto>
{
    public async Task<VoucherResponseDto> Handle(AssignVoucherCommand request, CancellationToken cancellationToken)
    {
        var registration = await registrationRepository.GetByIdAsync(request.Voucher.RegistrationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Registration), request.Voucher.RegistrationId);

        if (registration.Status != RegistrationStatus.Approved && registration.Status != RegistrationStatus.Completed)
        {
            throw new BusinessRuleException("Registration must be approved before voucher assignment.");
        }

        var eligibility = (await eligibilityRepository.FindAsync(record => record.RegistrationId == registration.RegistrationId, cancellationToken)).FirstOrDefault();
        if (eligibility is not null && !eligibility.IsEligible)
        {
            throw new BusinessRuleException("Eligibility must be passed before voucher assignment.");
        }

        var assessment = (await assessmentRepository.FindAsync(result => result.RegistrationId == registration.RegistrationId, cancellationToken)).FirstOrDefault();
        if (assessment is not null && assessment.Outcome != AssessmentOutcome.Passed)
        {
            throw new BusinessRuleException("Assessment must be passed before voucher assignment.");
        }

        var voucher = await voucherRepository.GetByIdAsync(request.Voucher.VoucherId, cancellationToken)
            ?? throw new NotFoundException(nameof(Voucher), request.Voucher.VoucherId);

        if (voucher.Status != VoucherStatus.Available)
        {
            throw new BusinessRuleException("Voucher must be available before assignment.");
        }

        voucher.AssignedToUserId = request.Voucher.AssignedToUserId;
        voucher.AssignedDate = DateTime.UtcNow;
        voucher.Status = VoucherStatus.Assigned;
        voucher.TokenizedLink = $"https://maverick.example.com/vouchers/{Guid.NewGuid():N}";
        voucherRepository.Update(voucher);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<VoucherResponseDto>(voucher);
    }
}

public sealed class VoucherLifecycleCommandHandlers(
    IVoucherRepository voucherRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) :
    IRequestHandler<DeliverVoucherCommand, VoucherResponseDto>,
    IRequestHandler<RevokeVoucherCommand, VoucherResponseDto>,
    IRequestHandler<ReissueVoucherCommand, VoucherResponseDto>
{
    public Task<VoucherResponseDto> Handle(DeliverVoucherCommand request, CancellationToken cancellationToken)
        => UpdateAsync(request.VoucherId, VoucherStatus.Delivered, cancellationToken, voucher => voucher.DeliveryDate = DateTime.UtcNow);

    public Task<VoucherResponseDto> Handle(RevokeVoucherCommand request, CancellationToken cancellationToken)
        => UpdateAsync(request.VoucherId, VoucherStatus.Available, cancellationToken, voucher =>
        {
            voucher.AssignedToUserId = null;
            voucher.AssignedDate = null;
            voucher.DeliveryDate = null;
        });

    public Task<VoucherResponseDto> Handle(ReissueVoucherCommand request, CancellationToken cancellationToken)
        => UpdateAsync(request.VoucherId, VoucherStatus.Assigned, cancellationToken, voucher => voucher.TokenizedLink = $"https://maverick.example.com/vouchers/{Guid.NewGuid():N}");

    private async Task<VoucherResponseDto> UpdateAsync(Guid voucherId, VoucherStatus status, CancellationToken cancellationToken, Action<Voucher> mutate)
    {
        var voucher = await voucherRepository.GetByIdAsync(voucherId, cancellationToken)
            ?? throw new NotFoundException(nameof(Voucher), voucherId);
        mutate(voucher);
        voucher.Status = status;
        voucherRepository.Update(voucher);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<VoucherResponseDto>(voucher);
    }
}

public sealed class VoucherQueryHandlers(
    IVoucherRepository voucherRepository,
    IMapper mapper) :
    IRequestHandler<GetVoucherByIdQuery, VoucherResponseDto>,
    IRequestHandler<GetAvailableVouchersQuery, IReadOnlyList<VoucherResponseDto>>,
    IRequestHandler<GetDriveVouchersQuery, IReadOnlyList<VoucherResponseDto>>
{
    public async Task<VoucherResponseDto> Handle(GetVoucherByIdQuery request, CancellationToken cancellationToken)
    {
        var voucher = await voucherRepository.GetByIdAsync(request.VoucherId, cancellationToken)
            ?? throw new NotFoundException(nameof(Voucher), request.VoucherId);
        return mapper.Map<VoucherResponseDto>(voucher);
    }

    public async Task<IReadOnlyList<VoucherResponseDto>> Handle(GetAvailableVouchersQuery request, CancellationToken cancellationToken)
    {
        var vouchers = await voucherRepository.GetAvailableVouchersAsync(request.DriveId, cancellationToken);
        return mapper.Map<IReadOnlyList<VoucherResponseDto>>(vouchers);
    }

    public async Task<IReadOnlyList<VoucherResponseDto>> Handle(GetDriveVouchersQuery request, CancellationToken cancellationToken)
    {
        var vouchers = await voucherRepository.FindAsync(voucher => voucher.DriveId == request.DriveId, cancellationToken);
        return mapper.Map<IReadOnlyList<VoucherResponseDto>>(vouchers);
    }
}
