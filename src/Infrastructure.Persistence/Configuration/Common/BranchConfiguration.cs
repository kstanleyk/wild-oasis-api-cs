using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildOasis.Domain.Entity.Common;

namespace WildOasis.Infrastructure.Persistence.Configuration.Common;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> entity)
    {
        entity.HasKey(e => new { e.Tenant, e.Code });

        entity.Property(e => e.Tenant).HasMaxLength(10).IsUnicode(false);
        entity.Property(e => e.Code).HasMaxLength(5).IsUnicode(false);
        entity.Property(e => e.Description).IsRequired().HasMaxLength(75).IsUnicode(false);
        entity.Property(e => e.BranchName).IsRequired().HasMaxLength(75).IsUnicode(false);
        entity.Property(e => e.BranchShortName).IsRequired().HasMaxLength(35).IsUnicode(false);
        entity.Property(e => e.StationCode).IsRequired().HasMaxLength(10).IsUnicode(false);
        entity.Property(e => e.Address).IsRequired().HasMaxLength(50).IsUnicode(false);
        entity.Property(e => e.Telephone).IsRequired().HasMaxLength(35).IsUnicode(false);
        entity.Property(e => e.Region).IsRequired().HasMaxLength(2).IsUnicode(false);
        entity.Property(e => e.Motto).IsRequired().HasMaxLength(150).IsUnicode(false);
        entity.Property(e => e.HeadOffice).IsRequired().HasMaxLength(2).IsUnicode(false);
        entity.Property(e => e.Employer).IsRequired().HasMaxLength(5).IsUnicode(false);
        entity.Property(e => e.BranchType).IsRequired().HasMaxLength(2).IsUnicode(false);
        entity.Property(e => e.BranchTown).IsRequired().HasMaxLength(35).IsUnicode(false);

        entity.ToTable("branch", "common");
    }
}