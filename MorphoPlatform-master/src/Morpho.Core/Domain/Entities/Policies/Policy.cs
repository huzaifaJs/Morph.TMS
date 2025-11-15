using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace Morpho.Domain.Entities.Policies
{
    public class Policy : FullAuditedAggregateRoot<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public bool IsActive { get; protected set; } = true;

        // Navigation
        public virtual ICollection<PolicyRule> Rules { get; protected set; }

        protected Policy()
        {
            Rules = new List<PolicyRule>();
        }

        public Policy(
            int tenantId,
            string name,
            string description = null,
            bool isActive = true)
        {
            TenantId = tenantId;
            Name = name;
            Description = description;
            IsActive = isActive;

            Rules = new List<PolicyRule>();
        }

        // Domain behavior methods
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void UpdateDetails(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
