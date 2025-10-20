using System.Reflection;
using System.Text;

using Courta.Application.Utilities;
using Courta.Domain.Entities.Authentication;
using Courta.Domain.Enums;
using Courta.WebAPI.Validators.Commands.Authentication;

using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Courta.WebAPI;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Presentation layer services such as controllers, MediatR, FluentValidation, and any pipeline behaviors.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection for chaining.</returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register FluentValidation validators found in the current assembly.
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<RefreshTokenCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<LogoutCommandValidator>();

        services.AddHttpContextAccessor();

        services.AddLogging(configure =>
        {
            configure.ClearProviders();
            configure.AddConsole();
            configure.AddDebug();
        });

        // Optionally, register pipeline behaviors (for example, a transactional behavior).
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        // Configure JWT authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetRequiredSetting("Jwt:Issuer"),
                ValidAudience = configuration.GetRequiredSetting("Jwt:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetRequiredSetting("Jwt:JwtSecretKey")))
            };
        });

        services.AddAuthorization(options =>
        {
            // -----------------------------------------------------------------------
            // 1. User & Identity Policies
            // -----------------------------------------------------------------------
            options.AddPolicy(PermissionConstants.UserSelfRead, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.UserSelfRead);
            });

            options.AddPolicy(PermissionConstants.UserSelfUpdate, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.UserSelfUpdate);
            });

            options.AddPolicy(PermissionConstants.TenantUserRead, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.TenantUserRead);
            });

            options.AddPolicy(PermissionConstants.TenantUserManage, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.TenantUserManage);
            });

            options.AddPolicy(PermissionConstants.ConfigManage, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.ConfigManage);
            });

            // NOTE: Token.Refresh is often the default UserPolicy, but defining it 
            // explicitly makes it clear that this permission is required.
            options.AddPolicy(PermissionConstants.TokenRefresh, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.TokenRefresh);
            });


            // -----------------------------------------------------------------------
            // 2. Court Management Policies
            // -----------------------------------------------------------------------
            options.AddPolicy(PermissionConstants.CourtRead, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.CourtRead);
            });

            options.AddPolicy(PermissionConstants.CourtCreate, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.CourtCreate);
            });

            options.AddPolicy(PermissionConstants.CourtUpdate, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.CourtUpdate);
            });

            options.AddPolicy(PermissionConstants.CourtDelete, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.CourtDelete);
            });


            // -----------------------------------------------------------------------
            // 3. Available Time Management Policies
            // -----------------------------------------------------------------------
            options.AddPolicy(PermissionConstants.TimeRead, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.TimeRead);
            });

            options.AddPolicy(PermissionConstants.TimeCreate, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.TimeCreate);
            });

            options.AddPolicy(PermissionConstants.TimeUpdate, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.TimeUpdate);
            });

            options.AddPolicy(PermissionConstants.TimeDelete, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(CustomClaims.Permission, PermissionConstants.TimeDelete);
            });
        });

        return services;
    }
}
