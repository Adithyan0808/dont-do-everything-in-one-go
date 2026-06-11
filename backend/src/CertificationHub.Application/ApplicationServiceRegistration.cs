using System.Reflection;
using CertificationHub.Application.Behaviors;
using CertificationHub.Application.DTOs;
using CertificationHub.Application.Rules;
using CertificationHub.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CertificationHub.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(_ => { }, assembly);
        services.AddValidatorsFromAssembly(assembly);

        if (configuration is not null)
        {
            services.Configure<AssessmentSettings>(configuration.GetSection(nameof(AssessmentSettings)));
        }
        else
        {
            services.AddOptions<AssessmentSettings>();
        }

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<ISlaService, SlaService>();
        services.AddScoped<IAuditService, AuditService>();

        services.AddScoped<EligibilityEngine>();
        services.AddScoped<IEligibilityRule, TenureEligibilityRule>();
        services.AddScoped<IEligibilityRule, TrainingCompletionRule>();
        services.AddScoped<IEligibilityRule, ManagerApprovalRule>();
        services.AddScoped<IEligibilityRule, PriorAttemptsRule>();
        services.AddScoped<IEligibilityRule, BudgetAvailabilityRule>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
