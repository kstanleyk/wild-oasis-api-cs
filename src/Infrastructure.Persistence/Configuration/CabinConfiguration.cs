using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildOasis.Domain.Entity;

namespace WildOasis.Infrastructure.Persistence.Configuration;

public class CabinConfiguration : IEntityTypeConfiguration<Cabin>
{
    public void Configure(EntityTypeBuilder<Cabin> entity)
    {
        entity.HasKey(e => new { e.Id });

        entity.Property(e => e.Id).HasMaxLength(5).IsUnicode(false);
        entity.Property(e => e.Name).HasMaxLength(20).IsUnicode(false);
        entity.Property(e => e.Description).IsRequired().HasMaxLength(1000).IsUnicode(false);
        entity.Property(e => e.Image).IsRequired().HasMaxLength(200).IsUnicode(false);

        entity.ToTable("cabin", "oasis");
    }
}