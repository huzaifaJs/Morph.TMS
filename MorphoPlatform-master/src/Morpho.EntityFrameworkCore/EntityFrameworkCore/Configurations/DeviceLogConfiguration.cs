using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities.Logs;

namespace Morpho.EntityFrameworkCore.Configurations
{
    public class DeviceLogConfiguration : IEntityTypeConfiguration<DeviceLog>
    {
        public void Configure(EntityTypeBuilder<DeviceLog> builder)
        {
            builder.ToTable("device_logs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId)
                .IsRequired();

            builder.Property(x => x.DeviceId)
                .IsRequired();

            builder.Property(x => x.Severity)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Message)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(x => x.TimestampUtc)
                .IsRequired();

            builder.HasIndex(x => x.DeviceId);
        }
    }
}
