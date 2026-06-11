using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class CertificationDriveConfiguration : IEntityTypeConfiguration<CertificationDrive>
{
    public void Configure(EntityTypeBuilder<CertificationDrive> builder)
    {
        builder.ToTable("CertificationDrives");
        builder.HasKey(drive => drive.DriveId);
        builder.ConfigureAuditableEntity();

        builder.Property(drive => drive.DriveCode).HasMaxLength(50).IsRequired();
        builder.Property(drive => drive.DriveName).HasMaxLength(250).IsRequired();
        builder.Property(drive => drive.VendorName).HasMaxLength(150).IsRequired();
        builder.Property(drive => drive.CertificationName).HasMaxLength(250).IsRequired();
        builder.Property(drive => drive.SponsorName).HasMaxLength(200);
        builder.Property(drive => drive.BudgetAllocated).HasColumnType("decimal(18,2)");
        builder.Property(drive => drive.BudgetConsumed).HasColumnType("decimal(18,2)");
        builder.Property(drive => drive.PolicyUrl).HasMaxLength(1000);
        builder.Property(drive => drive.Status).HasConversion<string>().HasMaxLength(50).IsRequired();

        builder.HasIndex(drive => drive.DriveCode).IsUnique();
        builder.HasIndex(drive => drive.Status);
        builder.HasIndex(drive => drive.VendorName);
        builder.HasIndex(drive => drive.RegistrationStartDate);

        builder.HasMany(drive => drive.Registrations)
            .WithOne(registration => registration.Drive)
            .HasForeignKey(registration => registration.DriveId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(drive => drive.Vouchers)
            .WithOne(voucher => voucher.Drive)
            .HasForeignKey(voucher => voucher.DriveId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(drive => drive.AuditLogs)
            .WithOne(auditLog => auditLog.Drive)
            .HasForeignKey(auditLog => auditLog.DriveId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
