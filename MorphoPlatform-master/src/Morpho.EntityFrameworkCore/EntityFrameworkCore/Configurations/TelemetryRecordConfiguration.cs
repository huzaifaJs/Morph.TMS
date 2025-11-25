using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities.Shipments;
using Morpho.Domain.Entities.Telemetry;

namespace Morpho.EntityFrameworkCore.EntityConfigurations
{
    public class TelemetryRecordConfiguration : IEntityTypeConfiguration<TelemetryRecord>
    {
        public void Configure(EntityTypeBuilder<TelemetryRecord> builder)
        {
            builder.ToTable("TelemetryRecords");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.TenantId).IsRequired();

            builder.Property(t => t.TimestampRaw)
                .HasColumnName("TimestampRaw")
                .IsRequired();

            builder.Property(t => t.TimestampUtc)
                .HasColumnName("TimestampUtc")
                .IsRequired();

            builder.Property(t => t.FirmwareVersion)
                .HasMaxLength(128);

            builder.Property(t => t.IpAddress)
                .HasMaxLength(64);

            // Sensor fields
            builder.Property(t => t.Rssi);
            builder.Property(t => t.BatteryLevel);
            builder.Property(t => t.Temperature);
            builder.Property(t => t.Humidity);
            builder.Property(t => t.MeanVibration);
            builder.Property(t => t.Light);

            builder.Property(t => t.Status)
                .HasMaxLength(64);

            builder.Property(t => t.Nbrfid);

            // NEW fields
            builder.Property(t => t.DeviceState).HasMaxLength(64);
            builder.Property(t => t.DeviceMode).HasMaxLength(64);
            builder.Property(t => t.ConnectionType).HasMaxLength(64);
            builder.Property(t => t.SignalQuality).HasMaxLength(64);

            builder.Property(t => t.Pressure);
            builder.Property(t => t.Co2);
            builder.Property(t => t.Voc);
            builder.Property(t => t.Speed);
            builder.Property(t => t.Altitude);
            builder.Property(t => t.Accuracy);

            // GPS Owned Value Object
            builder.OwnsOne(t => t.Gps, gps =>
            {
                gps.Property(g => g.Latitude).HasColumnName("GpsLatitude");
                gps.Property(g => g.Longitude).HasColumnName("GpsLongitude");
            });

            // Shipment FK
            builder.HasOne<Shipment>()
                .WithMany()
                .HasForeignKey(t => t.ShipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Container FK
            builder.HasOne<Container>()
                .WithMany()
                .HasForeignKey(t => t.ContainerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
