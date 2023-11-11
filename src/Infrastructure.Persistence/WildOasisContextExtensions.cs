using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WildOasis.Infrastructure.Persistence.Configuration.Common;

namespace WildOasis.Infrastructure.Persistence;

public static class WildOasisContextExtensions
{

    public static void AddEntityConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.FullName).HasMaxLength(75);
            entity.Property(e => e.Organization).HasMaxLength(5);
            entity.Property(e => e.Locale).HasMaxLength(5);
            entity.Property(e => e.UserCode).HasMaxLength(10);
            entity.Property(e => e.ImageUrl).HasMaxLength(200);
        });

        var configurationAssembly = typeof(BranchConfiguration).GetTypeInfo().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(configurationAssembly);
    }

}