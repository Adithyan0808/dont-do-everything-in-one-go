using CertificationHub.Application.Features.Notifications.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Notifications.Queries;

public sealed record GetNotificationsByUserQuery(Guid UserId, bool UnreadOnly = false) : IRequest<IReadOnlyList<NotificationSummaryDto>>;
