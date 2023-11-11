using System;
using Autofac;
using Core.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VishiHolding.Infrastructure.Reporting;
using WildOasis.API.Core;
using WildOasis.API.Middleware;
using WildOasis.API.Services;
using WildOasis.Application;
using WildOasis.Infrastructure.Persistence;

namespace WildOasis.API;

public class Startup
{
    private const string AllowedCorsOrigins = "AllowedCorsOrigins";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        AddSwagger(services);

        services.AddControllersWithViews(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            config.Filters.Add(new AuthorizeFilter(policy));

            var performanceFilter = new TrackPerformanceFilter("wild-oasis-api", "Core API");
            config.Filters.Add(performanceFilter);
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration["RedisCacheUrl"];
        });

        services.AddApplicationCoreServices();
        services.AddInfrastructureServices(Configuration);

        services
            .AddDatabaseContext(Configuration)
            .AddAuthorizationServer(Configuration)
            .AddCaching();

        var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? Array.Empty<string>();

        services.AddCors(options =>
        {
            options.AddPolicy(AllowedCorsOrigins,
                builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        if (Configuration.IsMessageProcessingEnabled())
            services.AddHostedService<TransactionMessageProcessingService>();
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Wild Oasis API",
            });

            c.OperationFilter<FileResultContentTypeOperationFilter>();
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        app.UseMiddleware<ExceptionMiddleware>();
        
        if (!env.IsDevelopment()) app.UseHsts();

        InitializeDatabase(app);

        app.UseRouting();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseCors(AllowedCorsOrigins);

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wild Oasis API");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterApplicationDependencies();
    }

    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        serviceScope?.ServiceProvider.GetRequiredService<WildOasisContext>().Database.Migrate();
    }
}