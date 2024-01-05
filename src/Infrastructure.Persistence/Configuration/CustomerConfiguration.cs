using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildOasis.Domain.Entity;

namespace WildOasis.Infrastructure.Persistence.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.HasKey(e => new { e.Id });

        entity.Property(e => e.Id).HasMaxLength(10).IsUnicode(false);
        entity.Property(e => e.FullName).HasMaxLength(200).IsUnicode(false);
        entity.Property(e => e.Email).IsRequired().HasMaxLength(75).IsUnicode(false);
        entity.Property(e => e.Nationality).IsRequired().HasMaxLength(35).IsUnicode(false);
        entity.Property(e => e.NationalId).IsRequired().HasMaxLength(75).IsUnicode(false);
        entity.Property(e => e.CountryFlag).IsRequired().HasMaxLength(75).IsUnicode(false);

        entity.ToTable("customer", "oasis");
    }
}