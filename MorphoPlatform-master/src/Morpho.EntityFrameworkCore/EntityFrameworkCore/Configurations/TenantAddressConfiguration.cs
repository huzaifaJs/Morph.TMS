using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Common;
using Morpho.Domain.Entities;
using Morpho.Domain.Enums;
using System;

namespace Morpho.EntityFrameworkCore.Configurations
{

    public class TenantAddressConfiguration : IEntityTypeConfiguration<TenantAddress>
    {
        public void Configure(EntityTypeBuilder<TenantAddress> builder)
        {
            builder.ToTable("TenantAddresses", "TenantManagement");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId).IsRequired(true);

            builder.Property(x => x.Type)
                .IsRequired(true)
                .HasConversion(
                    v => v.ToString(),
                    v => (TenantAddressType)Enum.Parse(typeof(TenantAddressType), v));

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.AddressLine1).IsRequired(true).HasColumnName(nameof(Address.AddressLine1)).HasMaxLength(256);
                address.Property(a => a.AddressLine2).IsRequired(false).HasColumnName(nameof(Address.AddressLine2)).HasMaxLength(256);
                address.Property(a => a.City).IsRequired(false).HasColumnName(nameof(Address.City)).HasMaxLength(128);
                address.Property(a => a.StateProvince).IsRequired(false).HasColumnName(nameof(Address.StateProvince)).HasMaxLength(128);
                address.Property(a => a.PostalCode).IsRequired(false).HasColumnName(nameof(Address.PostalCode)).HasMaxLength(32);
                address.Property(a => a.CountryCode).IsRequired(true).HasColumnName(nameof(Address.CountryCode)).HasMaxLength(2);
                address.Property(a => a.IsPrimary).IsRequired(true).HasColumnName(nameof(Address.IsPrimary));
            }).Navigation(x => x.Address).IsRequired();

             builder.HasOne(x => x.Tenant)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
