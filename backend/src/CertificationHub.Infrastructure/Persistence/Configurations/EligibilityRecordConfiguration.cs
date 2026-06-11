using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class EligibilityRecordConfiguration : IEntityTypeConfiguration<EligibilityRecord>
{
    public void Configure(EntityTypeBuilder<EligibilityRecord> builder)
    {
        builder.ToTable("EligibilityRecords");
        builder.HasKey(eligibility => eligibility.EligibilityId);
        builder.ConfigureAuditableEntity();

        builder.Property(eligibility => eligibility.CriteriaJson).IsRequired();
        builder.Property(eligibility => eligibility.DecisionReason).HasMaxLength(1000);
        builder.Property(eligibility => eligibility.Remarks).HasMaxLength(1000);

        builder.HasIndex(eligibility => eligibility.RegistrationId).IsUnique();

        builder.HasOne(eligibility => eligibility.Registration)
            .WithOne(registration => registration.EligibilityRecord)
            .HasForeignKey<EligibilityRecord>(eligibility => eligibility.RegistrationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
