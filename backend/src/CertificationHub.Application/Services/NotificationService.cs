using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Services;

public sealed class NotificationService(
    INotificationRepository notificationRepository,
    IApplicationUnitOfWork unitOfWork) : INotificationService
{
    public async Task SendAsync(Guid userId, string message, NotificationType type, CancellationToken cancellationToken = default)
    {
        await notificationRepository.AddAsync(new Notification
        {
            NotificationId = Guid.NewGuid(),
            UserId = userId,
            Message = message,
            Type = type,
            SLAStatus = SLAStatus.OnTrack,
            CreatedBy = "application"
        }, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task SendRegistrationAcknowledgementAsync(Guid userId, Guid registrationId, CancellationToken cancellationToken = default)
        => SendAsync(userId, $"Registration {registrationId} has been received.", NotificationType.RegistrationUpdate, cancellationToken);

    public Task SendEligibilityNotificationAsync(Guid userId, bool isEligible, CancellationToken cancellationToken = default)
        => SendAsync(userId, isEligible ? "Eligibility check passed." : "Eligibility check failed.", NotificationType.RegistrationUpdate, cancellationToken);

    public Task SendAssessmentNotificationAsync(Guid userId, string outcome, CancellationToken cancellationToken = default)
        => SendAsync(userId, $"Assessment result: {outcome}.", NotificationType.AssessmentResult, cancellationToken);

    public Task SendVoucherNotificationAsync(Guid userId, Guid voucherId, CancellationToken cancellationToken = default)
        => SendAsync(userId, $"Voucher {voucherId} has been assigned.", NotificationType.VoucherAssigned, cancellationToken);

    public Task SendReminderAsync(Guid userId, string message, CancellationToken cancellationToken = default)
        => SendAsync(userId, message, NotificationType.SLAAlert, cancellationToken);
}
