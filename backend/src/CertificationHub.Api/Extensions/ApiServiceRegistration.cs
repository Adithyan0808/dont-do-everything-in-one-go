using System.Reflection;
using System.Threading.RateLimiting;
using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Api.Filters;
using CertificationHub.Api.HealthChecks;
using CertificationHub.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace CertificationHub.Api.Extensions;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ValidateModelFilter>();
        services.AddScoped<AuditActionFilter>();

        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelFilter>();
            options.Filters.Add<AuditActionFilter>();
        });

        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Maverick Certification Hub API",
                Version = "v1",
                Description = "Enterprise API for certification drive automation."
            });

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter a valid JWT bearer token."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }

            options.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] ?? "API" });
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.AdminOnly, policy =>
                policy.RequireRole(AuthorizationRoles.Admin, AuthorizationRoles.SuperAdmin));
            options.AddPolicy(AuthorizationPolicies.CoordinatorOrAdmin, policy =>
                policy.RequireRole(AuthorizationRoles.Coordinator, AuthorizationRoles.Admin, AuthorizationRoles.SuperAdmin));
            options.AddPolicy(AuthorizationPolicies.ApproverOnly, policy =>
                policy.RequireRole(AuthorizationRoles.Approver, AuthorizationRoles.Manager, AuthorizationRoles.Admin, AuthorizationRoles.SuperAdmin));
            options.AddPolicy(AuthorizationPolicies.CandidateOnly, policy =>
                policy.RequireRole(AuthorizationRoles.Candidate, AuthorizationRoles.Employee));
            options.AddPolicy(AuthorizationPolicies.AuthenticatedUser, policy =>
                policy.RequireAuthenticatedUser());
        });

        services.AddCors(options =>
        {
            options.AddPolicy("ReactFrontend", policy =>
            {
                policy.WithOrigins(configuration["Frontend:BaseUrl"] ?? "http://localhost:5173")
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddRateLimiter(options =>
        {
            options.AddPolicy("LoginPolicy", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    }));

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 120,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 20
                    }));
        });

        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("database")
            .AddDbContextCheck<ApplicationDbContext>("dbcontext");

        return services;
    }
}
