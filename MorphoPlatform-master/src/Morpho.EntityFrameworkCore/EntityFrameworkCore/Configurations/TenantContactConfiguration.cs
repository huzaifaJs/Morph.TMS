namespace Morpho.EntityFrameworkCore.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Morpho.Domain.Common;
    using Morpho.Domain.Entities;

    public class TenantContactConfiguration : IEntityTypeConfiguration<TenantContact>
    {
        public void Configure(EntityTypeBuilder<TenantContact> builder)
        {
            builder.ToTable("TenantContacts", "TenantManagement");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId).IsRequired(true);

            builder.OwnsOne(
                p => p.Contact,
                contact =>
                {
                    contact.Property(x => x.Role).IsRequired(true).HasColumnName(nameof(Contact.Role)).HasMaxLength(256);
                    contact.Property(x => x.FullName).IsRequired(true).HasColumnName(nameof(Contact.FullName)).HasMaxLength(128);
                    contact.Property(x => x.Email).IsRequired(true).HasColumnName(nameof(Contact.Email)).HasMaxLength(256);
                    contact.Property(x => x.Phone).IsRequired(false).HasColumnName(nameof(Contact.Phone)).HasMaxLength(32);
                    contact.Property(x => x.IsPrimary).IsRequired(true).HasColumnName(nameof(Contact.IsPrimary));
                }).Navigation(x => x.Contact).IsRequired();

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.TenantId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
