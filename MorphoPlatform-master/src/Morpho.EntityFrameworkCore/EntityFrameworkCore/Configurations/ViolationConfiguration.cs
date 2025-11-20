using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities.Policies;

namespace Morpho.EntityFrameworkCore.Configurations
{
    public class ViolationConfiguration : IEntityTypeConfiguration<Violation>
    {
        public void Configure(EntityTypeBuilder<Violation> builder)
        {
            builder.ToTable("violations");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedOnAdd();

            builder.Property(v => v.DeviceId).IsRequired();
            builder.Property(v => v.PolicyRuleId).IsRequired();
            builder.Property(v => v.TenantId).IsRequired();

            // FIX — property name corrected
            builder.Property(v => v.OccurredAt).IsRequired();

            builder.Property(v => v.SensorType)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(v => v.Value)
                  .HasPrecision(18, 6);

            builder.Property(v => v.Status)
                  .HasMaxLength(30);
        }
    }
}
