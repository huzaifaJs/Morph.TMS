using Microsoft.EntityFrameworkCore;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Entities.Policies;
using Morpho.Domain.Entities.Telemetry;
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

        private static void ConfigureIoTDevice(ModelBuilder builder)
        {
            var entity = builder.Entity<IoTDevice>();

            entity.ToTable("iot_devices");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.TenantId)
                .IsRequired();

            entity.Property(x => x.Name)
                .HasMaxLength(200);

            entity.Property(x => x.SerialNumber)
                .HasMaxLength(100);

            entity.Property(x => x.DeviceType)
                .HasMaxLength(50);

            entity.Property(x => x.IsActive)
                .HasDefaultValue(true);

            entity.OwnsOne(x => x.LastKnownLocation, nav =>
            {
                nav.Property(p => p.Latitude).HasColumnName("last_latitude");
                nav.Property(p => p.Longitude).HasColumnName("last_longitude");
                nav.Property(p => p.Altitude).HasColumnName("last_altitude");
                nav.Property(p => p.Accuracy).HasColumnName("last_accuracy");
            });

            entity.HasIndex(x => new { x.TenantId, x.SerialNumber }).IsUnique();
        }

        private static void ConfigureTelemetry(ModelBuilder builder)
        {
            var entity = builder.Entity<TelemetryRecord>();

            entity.ToTable("telemetry");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.Property(x => x.DeviceId).IsRequired();

            entity.Property(x => x.Timestamp)
                .IsRequired();

            entity.Property(x => x.SensorType)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.Value)
                .HasPrecision(18, 6);   // numeric(18,6) in Postgres

            entity.Property(x => x.Unit)
                .HasMaxLength(20);

            entity.HasIndex(x => new { x.DeviceId, x.Timestamp });
            entity.HasOne<IoTDevice>()
                .WithMany(d => d.TelemetryRecords)
                .HasForeignKey(x => x.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigurePolicy(ModelBuilder builder)
        {
            var policy = builder.Entity<Policy>();
            policy.ToTable("policies");
            policy.HasKey(x => x.Id);

            policy.Property(x => x.Id).ValueGeneratedOnAdd();
            policy.Property(x => x.TenantId).IsRequired();
            policy.Property(x => x.Name).HasMaxLength(200).IsRequired();
            policy.Property(x => x.Description);

            policy.HasMany(x => x.Rules)
                  .WithOne(r => r.Policy)
                  .HasForeignKey(r => r.PolicyId)
                  .OnDelete(DeleteBehavior.Cascade);

            var rule = builder.Entity<PolicyRule>();
            rule.ToTable("policy_rules");
            rule.HasKey(x => x.Id);

            rule.Property(x => x.Id).ValueGeneratedOnAdd();
            rule.Property(x => x.SensorType)
                .HasMaxLength(50)
                .IsRequired();

            rule.Property(x => x.ConditionType)
                .IsRequired();

            // ThresholdRange as owned value object
            rule.OwnsOne(x => x.Threshold, nav =>
            {
                nav.Property(p => p.Min)
                    .HasColumnName("threshold_min")
                    .HasPrecision(18, 6);
                nav.Property(p => p.Max)
                    .HasColumnName("threshold_max")
                    .HasPrecision(18, 6);
            });
        }

        private static void ConfigureViolation(ModelBuilder builder)
        {
            var entity = builder.Entity<Violation>();

            entity.ToTable("violations");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.Property(x => x.DeviceId).IsRequired();
            entity.Property(x => x.PolicyRuleId).IsRequired();
            entity.Property(x => x.OccurredAt).IsRequired();

            entity.Property(x => x.SensorType)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(x => x.Value)
                  .HasPrecision(18, 6);

            entity.Property(x => x.Status)
                  .HasMaxLength(30);

            entity.HasOne<IoTDevice>()
                .WithMany(d => d.Violations)
                .HasForeignKey(x => x.DeviceId);

            entity.HasOne<PolicyRule>()
                .WithMany()
                .HasForeignKey(x => x.PolicyRuleId);
        }
    }
}
