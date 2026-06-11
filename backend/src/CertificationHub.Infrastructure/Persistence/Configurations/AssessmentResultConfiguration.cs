using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class AssessmentResultConfiguration : IEntityTypeConfiguration<AssessmentResult>
{
    public void Configure(EntityTypeBuilder<AssessmentResult> builder)
    {
        builder.ToTable("AssessmentResults");
        builder.HasKey(result => result.ResultId);
        builder.ConfigureAuditableEntity();

        builder.Property(result => result.Outcome).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(result => result.EvidenceUrl).HasMaxLength(1000);
        builder.Property(result => result.SubmittedBy).HasMaxLength(256);

        builder.HasIndex(result => result.RegistrationId).IsUnique();
        builder.HasIndex(result => result.Outcome);

        builder.HasOne(result => result.Registration)
            .WithOne(registration => registration.AssessmentResult)
            .HasForeignKey<AssessmentResult>(result => result.RegistrationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
