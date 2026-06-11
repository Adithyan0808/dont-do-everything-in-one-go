using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");
        builder.HasKey(auditLog => auditLog.AuditLogId);
        builder.ConfigureBaseEntity();

        builder.Property(auditLog => auditLog.EntityName).HasMaxLength(200).IsRequired();
        builder.Property(auditLog => auditLog.EntityId).HasMaxLength(100).IsRequired();
        builder.Property(auditLog => auditLog.Action).HasMaxLength(100).IsRequired();
        builder.Property(auditLog => auditLog.ActorName).HasMaxLength(200);
        builder.Property(auditLog => auditLog.IPAddress).HasMaxLength(64);
        builder.Property(auditLog => auditLog.UserAgent).HasMaxLength(512);

        builder.HasIndex(auditLog => auditLog.Timestamp);
        builder.HasIndex(auditLog => auditLog.ActorId);
        builder.HasIndex(auditLog => new { auditLog.EntityName, auditLog.EntityId });

        builder.HasOne(auditLog => auditLog.Drive)
            .WithMany(drive => drive.AuditLogs)
            .HasForeignKey(auditLog => auditLog.DriveId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
