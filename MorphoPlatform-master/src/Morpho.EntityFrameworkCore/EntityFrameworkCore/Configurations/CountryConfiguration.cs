namespace Morpho.EntityFrameworkCore.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Morpho.Domain.Common;
    using Morpho.Domain.Entities;

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries", "public");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(2)
                .IsRequired(false);

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.NameEn)
                    .HasColumnName(nameof(FullName.NameEn))
                    .IsRequired(true)  // required
                    .HasMaxLength(128);

                name.Property(n => n.NameAr)
                    .HasColumnName(nameof(FullName.NameAr))
                    .IsRequired(false) // optional
                    .HasMaxLength(128);

                // ✅ Add unique index on NameEn
                name.HasIndex(n => n.NameEn)
                    .IsUnique();
            }).Navigation(x => x.Name).IsRequired();

            builder.Property(x => x.PhoneCode)
                .HasMaxLength(8)
                .IsRequired(false);
        }
    }

}
