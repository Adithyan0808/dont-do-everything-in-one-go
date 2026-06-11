using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Interfaces;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IReadOnlyList<Notification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken cancellationToken = default);

    Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default);
}
