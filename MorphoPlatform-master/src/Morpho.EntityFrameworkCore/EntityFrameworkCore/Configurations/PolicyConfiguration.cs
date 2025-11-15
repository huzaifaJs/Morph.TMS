using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities.Policies;

namespace Morpho.EntityFrameworkCore.Configurations
{
    public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.ToTable("policies");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.TenantId)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Description);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            // FIXED RELATIONSHIP
            builder.HasMany(p => p.Rules)
                .WithOne(r => r.Policy)
                .HasForeignKey(r => r.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => new { p.TenantId, p.Name })
                .IsUnique();
        }
    }
}
