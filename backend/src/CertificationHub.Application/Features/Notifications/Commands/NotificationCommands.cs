using CertificationHub.Application.Features.Notifications.DTOs;
using CertificationHub.Domain.Enums;
using MediatR;

namespace CertificationHub.Application.Features.Notifications.Commands;

public sealed record SendNotificationCommand(Guid UserId, string Message, NotificationType Type) : IRequest<NotificationDto>;

public sealed record MarkNotificationReadCommand(Guid NotificationId) : IRequest<NotificationDto>;
