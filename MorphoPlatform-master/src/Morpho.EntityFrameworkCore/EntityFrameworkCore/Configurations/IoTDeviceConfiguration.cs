using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities.IoT;

namespace Morpho.EntityFrameworkCore.Configurations
{
    public class IoTDeviceConfiguration : IEntityTypeConfiguration<IoTDevice>
    {
        public void Configure(EntityTypeBuilder<IoTDevice> builder)
        {
            builder.ToTable("iot_devices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId)
                   .IsRequired();

            builder.Property(x => x.MorphoDeviceId)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(x => x.SerialNumber)
                   .HasMaxLength(64);

            builder.Property(x => x.Name)
                   .HasMaxLength(128);

            builder.Property(x => x.DeviceType)
                   .HasMaxLength(64);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);
        }
    }
}
