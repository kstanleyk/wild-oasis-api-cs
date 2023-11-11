using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WildOasis.Domain.Entity.Common;

namespace WildOasis.Infrastructure.Persistence
{
    public class WildOasisContext : DbContext
    {
        private readonly IConfiguration  _configuration;

        public WildOasisContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Branch> BranchSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddEntityConfigurations();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var connectionString = _configuration["ConnectionStrings:WildOasisData"];
            optionsBuilder.UseNpgsql(connectionString)
                          .UseSnakeCaseNamingConvention();
        }
    }
}
