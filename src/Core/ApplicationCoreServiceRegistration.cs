using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace WildOasis.Application;

public static class ApplicationCoreServiceRegistration
{
    public static IServiceCollection AddApplicationCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}