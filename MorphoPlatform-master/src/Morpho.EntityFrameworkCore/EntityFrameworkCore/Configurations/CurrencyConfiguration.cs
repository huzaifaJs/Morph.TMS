namespace Morpho.EntityFrameworkCore.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Morpho.Domain.Common;
    using Morpho.Domain.Entities;

    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies", "public");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired(true)
                .HasMaxLength(3);

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
        }
    }
}
