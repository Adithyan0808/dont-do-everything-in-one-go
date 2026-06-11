using System.Linq.Expressions;
using CertificationHub.Domain.Common;
using CertificationHub.Domain.Entities;
using CertificationHub.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<EmployeeCertification> EmployeeCertifications => Set<EmployeeCertification>();

    public DbSet<CertificationDrive> CertificationDrives => Set<CertificationDrive>();

    public DbSet<Registration> Registrations => Set<Registration>();

    public DbSet<EligibilityRecord> EligibilityRecords => Set<EligibilityRecord>();

    public DbSet<AssessmentResult> AssessmentResults => Set<AssessmentResult>();

    public DbSet<Voucher> Vouchers => Set<Voucher>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        ApplySoftDeleteFilters(modelBuilder);
        ApplicationModelSeeder.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditValues();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditValues()
    {
        var now = DateTime.UtcNow;
        const string currentUser = "system";

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = now;
                entry.Entity.CreatedBy = string.IsNullOrWhiteSpace(entry.Entity.CreatedBy)
                    ? currentUser
                    : entry.Entity.CreatedBy;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedDate = now;
                entry.Entity.ModifiedBy = string.IsNullOrWhiteSpace(entry.Entity.ModifiedBy)
                    ? currentUser
                    : entry.Entity.ModifiedBy;
            }
        }
    }

    private static void ApplySoftDeleteFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            var parameter = Expression.Parameter(entityType.ClrType, "entity");
            var property = Expression.Property(parameter, nameof(AuditableEntity.IsDeleted));
            var compare = Expression.Equal(property, Expression.Constant(false));
            var lambda = Expression.Lambda(compare, parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }
    }
}
