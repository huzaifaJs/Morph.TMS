using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities.Telemetry;

namespace Morpho.EntityFrameworkCore.EntityFrameworkCore.Configurations
{
    public class TelemetryRecordConfiguration : IEntityTypeConfiguration<TelemetryRecord>
    {
        public void Configure(EntityTypeBuilder<TelemetryRecord> builder)
        {
            builder.ToTable("telemetry_records");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId)
                   .IsRequired();

            builder.Property(x => x.DeviceId)
                   .IsRequired();

            builder.Property(x => x.SensorType)
                   .IsRequired()
                   .HasConversion<int>(); // store enum as int

            builder.Property(x => x.Value)
                   .HasColumnType("numeric(18,4)")
                   .IsRequired();

            builder.Property(x => x.Unit)
                   .HasMaxLength(32);

            builder.Property(x => x.Timestamp)
                   .IsRequired();

            // GPS as owned value object
            builder.OwnsOne(x => x.Gps, gps =>
            {
                gps.Property(g => g.Latitude).HasColumnName("gps_latitude");
                gps.Property(g => g.Longitude).HasColumnName("gps_longitude");
                gps.Property(g => g.Altitude).HasColumnName("gps_altitude");
                gps.Property(g => g.Accuracy).HasColumnName("gps_accuracy");
            });

            builder.Property(x => x.BatteryLevel)
                   .HasColumnType("numeric(5,2)");

            // indexes useful for queries
            builder.HasIndex(x => new { x.TenantId, x.DeviceId, x.Timestamp });
            builder.HasIndex(x => new { x.TenantId, x.ShipmentId, x.Timestamp });
        }
    }
}
