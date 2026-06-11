using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.UserId);
        builder.ConfigureAuditableEntity();

        builder.Property(user => user.EmployeeId).HasMaxLength(50).IsRequired();
        builder.Property(user => user.FullName).HasMaxLength(200).IsRequired();
        builder.Property(user => user.Email).HasMaxLength(256).IsRequired();
        builder.Property(user => user.Department).HasMaxLength(150);
        builder.Property(user => user.BusinessUnit).HasMaxLength(150);
        builder.Property(user => user.Location).HasMaxLength(150);
        builder.Property(user => user.ManagerEmail).HasMaxLength(256);
        builder.Property(user => user.PasswordHash).HasMaxLength(512).IsRequired();
        builder.Property(user => user.Role).HasConversion<string>().HasMaxLength(50).IsRequired();

        builder.HasIndex(user => user.EmployeeId).IsUnique();
        builder.HasIndex(user => user.Email).IsUnique();
        builder.HasIndex(user => user.Role);
        builder.HasIndex(user => user.IsActive);

        builder.HasMany(user => user.Registrations)
            .WithOne(registration => registration.User)
            .HasForeignKey(registration => registration.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(user => user.EmployeeCertifications)
            .WithOne(certification => certification.User)
            .HasForeignKey(certification => certification.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(user => user.Notifications)
            .WithOne(notification => notification.User)
            .HasForeignKey(notification => notification.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(user => user.Vouchers)
            .WithOne(voucher => voucher.AssignedToUser)
            .HasForeignKey(voucher => voucher.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
