using Autofac;
using WildOasis.API.Services;
using WildOasis.Application.Cabin;
using WildOasis.Infrastructure.Persistence;
using WildOasis.Infrastructure.Persistence.Repository;

namespace WildOasis.API.Core;

public static class AutofacExtensions
{
    public static void RegisterApplicationDependencies(this ContainerBuilder builder)
    {
        builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

        builder.RegisterType<EmailSenderService>().As<IEmailSenderService>().InstancePerLifetimeScope();

        builder.RegisterType<MessageSender>().As<IMessageSender>().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(CabinPersistence).Assembly)
            .Where(t => t.Name.EndsWith("Persistence"))
            .AsImplementedInterfaces().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(CabinService).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces().InstancePerLifetimeScope();
    }
}