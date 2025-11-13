using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities;
using Morpho.Domain.Enums;
using System;

namespace Morpho.EntityFrameworkCore.Configurations
{
    public class TenantProfileConfiguration : IEntityTypeConfiguration<TenantProfile>
    {
        public void Configure(EntityTypeBuilder<TenantProfile> builder)
        {
            builder.ToTable("TenantProfiles", "TenantManagement");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.LegalName)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.TradeName)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(x => x.RegistrationNumber)
                .HasMaxLength(128)
                .IsRequired(false);

            builder.Property(x => x.TaxNumber)
                .HasMaxLength(128)
                .IsRequired(false);

            builder.Property(x => x.Lei)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.EoriNumber)
                .HasMaxLength(32)
                .IsRequired(false);

            builder.Property(x => x.EstablishmentDate)
                .IsRequired(false);

            builder.Property(x => x.SupportEmail)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(x => x.SupportPhone)
                .HasMaxLength(32)
                .IsRequired(false);

            builder.Property(x => x.Website)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(x => x.Timezone)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.Property(x => x.BrandColor)
                .HasMaxLength(16)
                .IsRequired(false);

            builder.Property(x => x.LogoUrl)
                .HasMaxLength(512)
                .IsRequired(false);

            builder.Property(x => x.Notes)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .IsRequired(true)
                .HasConversion(
                    v => v.ToString(),
                    v => (ProfileStatus)Enum.Parse(typeof(ProfileStatus), v));

            builder.HasOne(x => x.CompanyType)
                .WithMany()
                .HasForeignKey(x => x.CompanyTypeId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Industry)
                .WithMany()
                .HasForeignKey(x => x.IndustryId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Language)
                .WithMany()
                .HasForeignKey(x => x.LanguageId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
