using CertificationHub.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CertificationHub.Infrastructure.Persistence.Configurations;

internal static class ConfigurationExtensions
{
    public static void ConfigureAuditableEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableEntity
    {
        builder.Ignore(entity => entity.Id);
        builder.Ignore(entity => entity.DomainEvents);
        builder.Property(entity => entity.CreatedBy).HasMaxLength(256).IsRequired();
        builder.Property(entity => entity.ModifiedBy).HasMaxLength(256);
        builder.Property(entity => entity.DeletedBy).HasMaxLength(256);
        builder.Property(entity => entity.RowVersion).IsRowVersion().IsConcurrencyToken();
        builder.HasIndex(entity => entity.IsDeleted);
    }

    public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntity
    {
        builder.Ignore(entity => entity.Id);
        builder.Ignore(entity => entity.DomainEvents);
        builder.Property(entity => entity.CreatedBy).HasMaxLength(256).IsRequired();
        builder.Property(entity => entity.ModifiedBy).HasMaxLength(256);
        builder.Property(entity => entity.RowVersion).IsRowVersion().IsConcurrencyToken();
    }
}
