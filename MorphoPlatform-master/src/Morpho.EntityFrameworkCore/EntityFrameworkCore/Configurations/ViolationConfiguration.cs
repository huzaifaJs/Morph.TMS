using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities.Policies;
using System;

namespace Morpho.EntityFrameworkCore.Configurations
{
    public class ViolationConfiguration : IEntityTypeConfiguration<Violation>
    {
        public void Configure(EntityTypeBuilder<Violation> builder)
        {
            builder.ToTable("violations");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedOnAdd();

            builder.Property(v => v.DeviceId).IsRequired();
            builder.Property(v => v.PolicyRuleId).IsRequired();
            builder.Property(v => v.TenantId).IsRequired();
            builder.Property(v => v.OccurredAtUtc).IsRequired();
        }
    }
}
