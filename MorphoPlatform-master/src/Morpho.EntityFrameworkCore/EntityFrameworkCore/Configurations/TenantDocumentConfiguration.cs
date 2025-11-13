namespace Morpho.EntityFrameworkCore.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Morpho.Domain.Entities;
    using Morpho.Domain.Enums;
    using System;

    public class TenantDocumentConfiguration : IEntityTypeConfiguration<TenantDocument>
    {
        public void Configure(EntityTypeBuilder<TenantDocument> builder)
        {
            builder.ToTable("TenantDocuments", "TenantManagement");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId)
                .IsRequired(true);

            builder.Property(x => x.DocType)
                .IsRequired(true)
                .HasConversion(
                    v => v.ToString(),
                    v => (DocumentType)Enum.Parse(typeof(DocumentType), v));

            builder.Property(x => x.Title)
                .HasMaxLength(256)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .IsRequired(false);

            builder.Property(x => x.MimeType)
                .IsRequired(false)
                .HasConversion(
                    v => v.ToString(),
                    v => (MimeType)Enum.Parse(typeof(MimeType), v));

            builder.Property(x => x.StorageKey)
                .HasMaxLength(512)
                .IsRequired(true);

            builder.Property(x => x.FileHash)
                .HasMaxLength(128)
                .IsRequired(false);

            builder.Property(x => x.IssuedBy)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(x => x.IssuedAt)
                .IsRequired(false);

            builder.Property(x => x.ExpiresAt)
                .IsRequired(false);

            builder.Property(x => x.Version)
                .HasMaxLength(64)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .IsRequired(true)
                .HasConversion(
                    v => v.ToString(),
                    v => (DocumentStatus)Enum.Parse(typeof(DocumentStatus), v));

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
