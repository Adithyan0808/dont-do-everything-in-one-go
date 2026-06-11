using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Infrastructure.Repositories;

public sealed class NotificationRepository(ApplicationDbContext dbContext)
    : GenericRepository<Notification>(dbContext), INotificationRepository
{
    public async Task<IReadOnlyList<Notification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(notification => notification.UserId == userId && !notification.IsRead)
            .OrderByDescending(notification => notification.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default)
    {
        var notification = await DbSet.FirstOrDefaultAsync(item => item.NotificationId == notificationId, cancellationToken);

        if (notification is null)
        {
            return;
        }

        notification.IsRead = true;
        notification.ReadDate = DateTime.UtcNow;
    }
}
