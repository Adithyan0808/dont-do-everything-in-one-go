using CertificationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

public sealed class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("Vouchers");
        builder.HasKey(voucher => voucher.VoucherId);
        builder.ConfigureAuditableEntity();

        builder.Property(voucher => voucher.Vendor).HasMaxLength(150);
        builder.Property(voucher => voucher.EncryptedCode).IsRequired();
        builder.Property(voucher => voucher.MaskedCode).HasMaxLength(100);
        builder.Property(voucher => voucher.Value).HasColumnType("decimal(18,2)");
        builder.Property(voucher => voucher.Status).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(voucher => voucher.TokenizedLink).HasMaxLength(1000);

        builder.HasIndex(voucher => voucher.EncryptedCode).IsUnique();
        builder.HasIndex(voucher => voucher.Status);
        builder.HasIndex(voucher => voucher.AssignedToUserId);

        builder.HasOne(voucher => voucher.Drive)
            .WithMany(drive => drive.Vouchers)
            .HasForeignKey(voucher => voucher.DriveId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(voucher => voucher.AssignedToUser)
            .WithMany(user => user.Vouchers)
            .HasForeignKey(voucher => voucher.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
