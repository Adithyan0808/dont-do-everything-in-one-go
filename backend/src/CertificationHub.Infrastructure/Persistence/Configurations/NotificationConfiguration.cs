using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");
        builder.HasKey(notification => notification.NotificationId);
        builder.ConfigureAuditableEntity();

        builder.Property(notification => notification.Message).HasMaxLength(2000).IsRequired();
        builder.Property(notification => notification.Type).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(notification => notification.SLAStatus).HasConversion<string>().HasMaxLength(50).IsRequired();

        builder.HasIndex(notification => notification.UserId);
        builder.HasIndex(notification => notification.IsRead);

        builder.HasOne(notification => notification.User)
            .WithMany(user => user.Notifications)
            .HasForeignKey(notification => notification.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
