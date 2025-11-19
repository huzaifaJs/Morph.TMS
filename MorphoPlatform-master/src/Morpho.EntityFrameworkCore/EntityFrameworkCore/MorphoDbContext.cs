using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Morpho.Authorization.Roles;
using Morpho.Authorization.Users;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.GeoFencing;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Entities.Logs;
using Morpho.Domain.Entities.Policies;
using Morpho.Domain.Entities.Telemetry;
using Morpho.EntityFrameworkCore.Configurations;
using Morpho.EntityFrameworkCore.EntityFrameworkCore.Configurations;
using Morpho.MultiTenancy;

namespace Morpho.EntityFrameworkCore
{
    public class MorphoDbContext : AbpZeroDbContext<Tenant, Role, User, MorphoDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<TenantProfile> TenantProfiles { get; set; }
        public DbSet<TenantDocument> TenantDocuments { get; set; }
        public DbSet<TenantContact> TenantContacts { get; set; }
        public DbSet<TenantAddress> TenantAddresses { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<VehicleTypes> VehicleTypes { get; set; }
        public DbSet<TelemetryRecord> TelemetryRecords { get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<IoTDevice> IoTDevices { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyRule> PolicyRules { get; set; }
        public DbSet<DeviceLog> DeviceLogs { get; set; }

        public virtual DbSet<Domain.Entities.DeviceConfig> DeviceConfigs { get; set; }
      //  public virtual DbSet<DeviceLog> DeviceLogs { get; set; }

        public virtual DbSet<GeoFenceZone> GeoFenceZones { get; set; }

        public MorphoDbContext(DbContextOptions<MorphoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MorphoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new IoTDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfigConfiguration());
           // modelBuilder.ApplyConfiguration(new DeviceLogConfiguration());
            modelBuilder.ApplyConfiguration(new TelemetryRecordConfiguration());
            // modelBuilder.ApplyConfiguration(new GeoFenceAreaConfiguration());
            modelBuilder.Entity<DeviceLog>(b =>
            {
                b.ToTable("device_logs");

                b.Property(x => x.Severity).HasMaxLength(50).IsRequired();
                b.Property(x => x.Message).HasMaxLength(2000).IsRequired();
                b.Property(x => x.TimestampUtc).IsRequired();

                b.HasIndex(x => x.DeviceId);
            });
            modelBuilder.ApplyConfiguration(new DeviceLogConfiguration());
            modelBuilder.Entity<IoTDevice>(b =>
            {
                b.OwnsOne(x => x.LastKnownLocation, gps =>
                {
                    gps.Property(p => p.Latitude).HasColumnName("LastKnown_Latitude");
                    gps.Property(p => p.Longitude).HasColumnName("LastKnown_Longitude");
                    gps.Property(p => p.Altitude).HasColumnName("LastKnown_Altitude");
                    gps.Property(p => p.Accuracy).HasColumnName("LastKnown_Accuracy");
                });
            });
        }
    }
}
