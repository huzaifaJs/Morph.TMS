using Microsoft.EntityFrameworkCore;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Entities.Policies;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Entities.Shipments;
using Morpho.Domain.ValueObjects;

namespace Morpho.EntityFrameworkCore
{
    public static class IoTModelBuilderExtensions
    {
        public static void ConfigureIoT(this ModelBuilder builder)
        {
            ConfigureIoTDevice(builder);
            ConfigureTelemetry(builder);
            ConfigurePolicy(builder);
            ConfigureViolation(builder);
        }

        // ============================================================
        // IOT DEVICE
        // ============================================================
        private static void ConfigureIoTDevice(ModelBuilder builder)
        {
            var entity = builder.Entity<IoTDevice>();

            entity.ToTable("iot_devices");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.Property(x => x.TenantId).IsRequired();

            entity.Property(x => x.Name).HasMaxLength(200);
            entity.Property(x => x.SerialNumber).HasMaxLength(100);
            entity.Property(x => x.DeviceType).HasMaxLength(50);

            entity.Property(x => x.MorphoDeviceId); // INT external ID

            entity.Property(x => x.IsActive).HasDefaultValue(true);

            // GPS (Owned)
            entity.OwnsOne(x => x.LastKnownLocation, nav =>
            {
                nav.Property(p => p.Latitude).HasColumnName("last_latitude");
                nav.Property(p => p.Longitude).HasColumnName("last_longitude");
                nav.Property(p => p.Altitude).HasColumnName("last_altitude");
                nav.Property(p => p.Accuracy).HasColumnName("last_accuracy");
            });

            entity.HasIndex(x => new { x.TenantId, x.SerialNumber }).IsUnique();
        }

        // ============================================================
        // TELEMETRY
        // ============================================================
        private static void ConfigureTelemetry(ModelBuilder builder)
        {
            var entity = builder.Entity<TelemetryRecord>();

            entity.ToTable("telemetry_records");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.Property(x => x.TenantId).IsRequired();
            entity.Property(x => x.DeviceId).IsRequired();

            // Timestamps
            entity.Property(x => x.TimestampRaw).IsRequired();
            entity.Property(x => x.TimestampUtc).IsRequired();

            // Basic metadata
            entity.Property(x => x.FirmwareVersion).HasMaxLength(128);
            entity.Property(x => x.IpAddress).HasMaxLength(64);

            // Sensor fields
            entity.Property(x => x.Rssi);
            entity.Property(x => x.BatteryLevel);
            entity.Property(x => x.Temperature);
            entity.Property(x => x.Humidity);
            entity.Property(x => x.MeanVibration);
            entity.Property(x => x.Light);

            // State
            entity.Property(x => x.Status).HasMaxLength(64);
            entity.Property(x => x.Nbrfid);

            // NEW FIELDS
            entity.Property(x => x.DeviceState).HasMaxLength(64);
            entity.Property(x => x.DeviceMode).HasMaxLength(64);
            entity.Property(x => x.ConnectionType).HasMaxLength(64);
            entity.Property(x => x.SignalQuality).HasMaxLength(64);

            entity.Property(x => x.Pressure);
            entity.Property(x => x.Co2);
            entity.Property(x => x.Voc);
            entity.Property(x => x.Speed);
            entity.Property(x => x.Altitude);
            entity.Property(x => x.Accuracy);

            // GPS — Owned VO
            entity.OwnsOne(x => x.Gps, gps =>
            {
                gps.Property(g => g.Latitude).HasColumnName("gps_latitude");
                gps.Property(g => g.Longitude).HasColumnName("gps_longitude");
            });

            // Shipment relationship
            entity.HasOne<Shipment>()
                .WithMany()
                .HasForeignKey(x => x.ShipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Container relationship
            entity.HasOne<Container>()
                .WithMany()
                .HasForeignKey(x => x.ContainerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Device -> Telemetry FK
            entity.HasOne<IoTDevice>()
                .WithMany(d => d.TelemetryRecords)
                .HasForeignKey(x => x.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(x => new { x.DeviceId, x.TimestampUtc });
        }

        // ============================================================
        // POLICY
        // ============================================================
        private static void ConfigurePolicy(ModelBuilder builder)
        {
            var policy = builder.Entity<Policy>();
            policy.ToTable("policies");

            policy.HasKey(x => x.Id);
            policy.Property(x => x.Id).ValueGeneratedOnAdd();
            policy.Property(x => x.TenantId).IsRequired();
            policy.Property(x => x.Name).HasMaxLength(200).IsRequired();

            policy.HasMany(x => x.Rules)
                  .WithOne(r => r.Policy)
                  .HasForeignKey(r => r.PolicyId)
                  .OnDelete(DeleteBehavior.Cascade);

            var rule = builder.Entity<PolicyRule>();
            rule.ToTable("policy_rules");

            rule.HasKey(x => x.Id);
            rule.Property(x => x.Id).ValueGeneratedOnAdd();

            rule.Property(x => x.SensorType).HasMaxLength(50).IsRequired();
            rule.Property(x => x.ConditionType).IsRequired();

            rule.OwnsOne(x => x.Threshold, nav =>
            {
                nav.Property(p => p.Min).HasColumnName("threshold_min");
                nav.Property(p => p.Max).HasColumnName("threshold_max");
            });
        }

        // ============================================================
        // POLICY VIOLATIONS (NEW MODEL)
        // ============================================================
        public static void ConfigureViolation(ModelBuilder builder)
        {
            builder.Entity<PolicyViolation>(entity =>
            {
                entity.ToTable("policy_violations");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.TenantId).IsRequired();
                entity.Property(x => x.PolicyId).IsRequired();
                entity.Property(x => x.RuleId).IsRequired();
                entity.Property(x => x.DeviceId).IsRequired();

                entity.Property(x => x.SensorType).IsRequired();
                entity.Property(x => x.Value).HasPrecision(10, 2).IsRequired();
                entity.Property(x => x.Unit).HasMaxLength(20);

                entity.Property(x => x.OccurredAtUtc).IsRequired();

                entity.Property(x => x.ShipmentId);
                entity.Property(x => x.ContainerId);

                // ================================
                // Device → PolicyViolations (1:N)
                // ================================
                entity.HasOne<IoTDevice>()
                      .WithMany(d => d.Violations)      // MUST be ICollection<PolicyViolation>
                      .HasForeignKey(x => x.DeviceId)
                      .OnDelete(DeleteBehavior.Cascade);

                // ================================
                // PolicyRule → PolicyViolations (1:N)
                // ================================
                entity.HasOne<PolicyRule>()
                      .WithMany(r => r.Violations)     // MUST be ICollection<PolicyViolation>
                      .HasForeignKey(x => x.RuleId)
                      .OnDelete(DeleteBehavior.Cascade);

                // ================================
                // Policy → PolicyViolation (1:N)
                // ================================
                entity.HasOne<Policy>()
                      .WithMany(p => p.Violations)     // MUST be ICollection<PolicyViolation>
                      .HasForeignKey(x => x.PolicyId);
            });
        }

    }
}
