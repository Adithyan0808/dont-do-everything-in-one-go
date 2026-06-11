using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.ToTable("Registrations");
        builder.HasKey(registration => registration.RegistrationId);
        builder.ConfigureAuditableEntity();

        builder.Property(registration => registration.ExamTrack).HasMaxLength(150);
        builder.Property(registration => registration.PreferredSlot).HasMaxLength(150);
        builder.Property(registration => registration.ApprovalComments).HasMaxLength(1000);
        builder.Property(registration => registration.Status).HasConversion<string>().HasMaxLength(50).IsRequired();

        builder.HasIndex(registration => registration.UserId);
        builder.HasIndex(registration => registration.DriveId);
        builder.HasIndex(registration => registration.Status);
        builder.HasIndex(registration => new { registration.UserId, registration.DriveId }).IsUnique();

        builder.HasOne(registration => registration.Drive)
            .WithMany(drive => drive.Registrations)
            .HasForeignKey(registration => registration.DriveId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(registration => registration.User)
            .WithMany(user => user.Registrations)
            .HasForeignKey(registration => registration.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(registration => registration.EligibilityRecord)
            .WithOne(eligibility => eligibility.Registration)
            .HasForeignKey<EligibilityRecord>(eligibility => eligibility.RegistrationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(registration => registration.AssessmentResult)
            .WithOne(result => result.Registration)
            .HasForeignKey<AssessmentResult>(result => result.RegistrationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(registration => registration.VoucherAllocation)
            .WithOne()
            .HasForeignKey<Voucher>("RegistrationId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
