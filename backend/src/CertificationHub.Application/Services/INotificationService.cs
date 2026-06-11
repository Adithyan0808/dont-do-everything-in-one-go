using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Services;

public interface INotificationService
{
    Task SendAsync(Guid userId, string message, NotificationType type, CancellationToken cancellationToken = default);

    Task SendRegistrationAcknowledgementAsync(Guid userId, Guid registrationId, CancellationToken cancellationToken = default);

    Task SendEligibilityNotificationAsync(Guid userId, bool isEligible, CancellationToken cancellationToken = default);

    Task SendAssessmentNotificationAsync(Guid userId, string outcome, CancellationToken cancellationToken = default);

    Task SendVoucherNotificationAsync(Guid userId, Guid voucherId, CancellationToken cancellationToken = default);

    Task SendReminderAsync(Guid userId, string message, CancellationToken cancellationToken = default);
}
