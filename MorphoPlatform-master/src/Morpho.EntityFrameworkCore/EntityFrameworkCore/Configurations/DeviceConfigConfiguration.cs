using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities;


namespace Morpho.EntityFrameworkCore.EntityFrameworkCore.Configurations
{
    public class DeviceConfigConfiguration : IEntityTypeConfiguration<DeviceConfig>
    {
        public void Configure(EntityTypeBuilder<DeviceConfig> builder)
        {
            builder.ToTable("device_configs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DeviceId).IsRequired();
            builder.Property(x => x.ConfiguredBy).HasMaxLength(128);

            builder.OwnsOne(x => x.Thresholds, t =>
            {
                t.Property(v => v.Min).HasColumnName("threshold_min");
                t.Property(v => v.Max).HasColumnName("threshold_max");
                t.Property(v => v.MaxVariation).HasColumnName("threshold_max_var");
            });

        }
    }
}
