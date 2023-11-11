using System;
using System.Text;
using Core.Common.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using WildOasis.Infrastructure.Persistence;

namespace WildOasis.API.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.TryAddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>)); // open generic registration
        services.TryAddSingleton<IDistributedCacheFactory, DistributedCacheFactory>();
        return services;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:WildOasisData"];
        services.AddDbContext<WildOasisContext>(o => o.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
        return services;
    }

    public static IServiceCollection AddAuthorizationServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<WildOasisContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}