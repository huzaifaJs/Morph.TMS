using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Morpho.Domain.Entities.Policies;

namespace Morpho.EntityFrameworkCore.Configurations
{
    public class PolicyRuleConfiguration : IEntityTypeConfiguration<PolicyRule>
    {
        public void Configure(EntityTypeBuilder<PolicyRule> builder)
        {
            builder.ToTable("policy_rules");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.TenantId).IsRequired();

            builder.Property(r => r.SensorType)
                .IsRequired();

            builder.Property(r => r.ConditionType)
                .IsRequired();

            // Value Object Mapping
            builder.OwnsOne(r => r.Threshold, t =>
            {
                t.Property(v => v.Min).HasColumnName("threshold_min");
                t.Property(v => v.Max).HasColumnName("threshold_max");
            });

            // Relation → Policy
            builder.HasOne(r => r.Policy)
                .WithMany(p => p.Rules)
                .HasForeignKey(r => r.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
