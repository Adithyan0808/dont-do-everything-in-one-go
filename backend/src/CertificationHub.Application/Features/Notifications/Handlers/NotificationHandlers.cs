using AutoMapper;
using CertificationHub.Application.Exceptions;
using CertificationHub.Application.Features.Notifications.Commands;
using CertificationHub.Application.Features.Notifications.DTOs;
using CertificationHub.Application.Features.Notifications.Queries;
using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using MediatR;

namespace CertificationHub.Application.Features.Notifications.Handlers;

public sealed class NotificationCommandHandlers(
    INotificationRepository notificationRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper) :
    IRequestHandler<SendNotificationCommand, NotificationDto>,
    IRequestHandler<MarkNotificationReadCommand, NotificationDto>
{
    public async Task<NotificationDto> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            NotificationId = Guid.NewGuid(),
            UserId = request.UserId,
            Message = request.Message,
            Type = request.Type,
            SLAStatus = SLAStatus.OnTrack,
            CreatedBy = "application"
        };

        await notificationRepository.AddAsync(notification, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<NotificationDto>(notification);
    }

    public async Task<NotificationDto> Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
    {
        await notificationRepository.MarkAsReadAsync(request.NotificationId, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var notification = await notificationRepository.GetByIdAsync(request.NotificationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Notification), request.NotificationId);
        return mapper.Map<NotificationDto>(notification);
    }
}

public sealed class GetNotificationsByUserQueryHandler(
    INotificationRepository notificationRepository,
    IMapper mapper) : IRequestHandler<GetNotificationsByUserQuery, IReadOnlyList<NotificationSummaryDto>>
{
    public async Task<IReadOnlyList<NotificationSummaryDto>> Handle(GetNotificationsByUserQuery request, CancellationToken cancellationToken)
    {
        var notifications = request.UnreadOnly
            ? await notificationRepository.GetUnreadNotificationsAsync(request.UserId, cancellationToken)
            : await notificationRepository.FindAsync(notification => notification.UserId == request.UserId, cancellationToken);

        return mapper.Map<IReadOnlyList<NotificationSummaryDto>>(notifications);
    }
}
