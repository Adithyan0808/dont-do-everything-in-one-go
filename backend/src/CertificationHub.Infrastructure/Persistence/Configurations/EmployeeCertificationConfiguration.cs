using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class EmployeeCertificationConfiguration : IEntityTypeConfiguration<EmployeeCertification>
{
    public void Configure(EntityTypeBuilder<EmployeeCertification> builder)
    {
        builder.ToTable("EmployeeCertifications");
        builder.HasKey(certification => certification.CertificationId);
        builder.ConfigureAuditableEntity();

        builder.Property(certification => certification.CertificationName).HasMaxLength(250).IsRequired();
        builder.Property(certification => certification.VendorName).HasMaxLength(150).IsRequired();
        builder.Property(certification => certification.CredentialId).HasMaxLength(150);

        builder.HasIndex(certification => certification.UserId);
        builder.HasIndex(certification => certification.CertificationName);

        builder.HasOne(certification => certification.User)
            .WithMany(user => user.EmployeeCertifications)
            .HasForeignKey(certification => certification.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
