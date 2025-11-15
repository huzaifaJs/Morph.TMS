using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Morpho.Authorization.Roles;
using Morpho.Authorization.Users;
using Morpho.MultiTenancy;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Entities.Policies;

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

        public MorphoDbContext(DbContextOptions<MorphoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MorphoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }
}
