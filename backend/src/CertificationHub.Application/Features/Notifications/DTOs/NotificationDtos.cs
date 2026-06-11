using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Features.Notifications.DTOs;

public sealed record NotificationDto(
    Guid NotificationId,
    Guid UserId,
    string Message,
    NotificationType Type,
    bool IsRead,
    SLAStatus SLAStatus,
    DateTime CreatedDate,
    DateTime? ReadDate);

public sealed record NotificationSummaryDto(Guid NotificationId, string Message, NotificationType Type, bool IsRead, DateTime CreatedDate);
