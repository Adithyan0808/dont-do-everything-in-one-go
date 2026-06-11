using CertificationHub.Application.Interfaces;
using CertificationHub.Infrastructure.Persistence;
using CertificationHub.Infrastructure.Persistence.Interceptors;
using CertificationHub.Infrastructure.Persistence.Seed;
using CertificationHub.Infrastructure.Repositories;
using CertificationHub.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CertificationHub.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddSingleton<SoftDeleteInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Data Source=certificationhub.db";
            var provider = configuration["Database:Provider"] ?? "Sqlite";

            if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                options.UseSqlServer(connectionString, sqlServer =>
                    sqlServer.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }
            else
            {
                options.UseSqlite(connectionString, sqlite =>
                    sqlite.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }

            options.AddInterceptors(
                serviceProvider.GetRequiredService<AuditableEntityInterceptor>(),
                serviceProvider.GetRequiredService<SoftDeleteInterceptor>());
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDriveRepository, DriveRepository>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IApplicationUnitOfWork>(serviceProvider =>
            (IApplicationUnitOfWork)serviceProvider.GetRequiredService<IUnitOfWork>());
        services.AddScoped<ApplicationDbSeeder>();

        return services;
    }
}
