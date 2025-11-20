using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Morpho.Authorization.Roles;
using Morpho.Authorization.Users;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.Devices;
using Morpho.Domain.Entities.FuelType;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Entities.GeoFencing;
using Morpho.Domain.Entities.Logs;
using Morpho.Domain.Entities.Policies;
using Morpho.Domain.Entities.ShipmentManagement;
using Morpho.Domain.Entities.ShipmentPackage;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Entities.VehicleContainer;
using Morpho.Domain.Entities.VehicleDocument;
using Morpho.Domain.Entities.VehicleDocumentType;
using Morpho.Domain.Entities.Vehicles;
using Morpho.MultiTenancy;
using Morpho.EntityFrameworkCore.Configurations;
using Morpho.EntityFrameworkCore.EntityFrameworkCore.Configurations;


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
        public DbSet<DeviceType> DeviceType { get; set; }
        public DbSet<TrackingDevices> TrackingDevices { get; set; }
        public DbSet<FuelType> FuelType { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<VehicleDocumentType> VehicleDocumentType { get; set; }
        public DbSet<VehicleDocument> VehicleDocument { get; set; }
        public DbSet<VehicleContainerType> VehicleContainerType { get; set; }
        public DbSet<VehicleContainer> VehicleContainer { get; set; }
        public DbSet<PackageType> PackageType { get; set; }
        public DbSet<ShipmentPackage> ShipmentPackage { get; set; }
        public DbSet<TMSShipment> TMSShipment { get; set; }
        public DbSet<TMSShipmentStatusLog> TMSShipmentStatusLog { get; set; }
        public DbSet<TMSShipmentContainerAssignment> TMSShipmentContainerAssignment { get; set; }
        public DbSet<TMSShipmentpackageAssignment> TMSShipmentpackageAssignment { get; set; }
        public DbSet<TMSIoTPolicyAssignment> TMSIoTPolicyAssignment { get; set; }
        public DbSet<TMSShipmentRouteAssignment> TMSShipmentRouteAssignment { get; set; }

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
