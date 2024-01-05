using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WildOasis.Infrastructure.Persistence.Configuration;

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

        modelBuilder.Entity<ApplicationUser>().ToTable("user", "identity");
        modelBuilder.Entity<IdentityRole>().ToTable("role", "identity");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("user_role", "identity");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("user_claim", "identity");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("user_login", "identity");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("role_claim", "identity");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("user_token","identity");

        var configurationAssembly = typeof(CabinConfiguration).GetTypeInfo().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(configurationAssembly);
    }

}