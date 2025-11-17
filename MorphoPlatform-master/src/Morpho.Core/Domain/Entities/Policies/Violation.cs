using Abp.Domain.Entities.Auditing;
using System;

namespace Morpho.Domain.Entities.Policies
{
    public class Violation : FullAuditedEntity<Guid>
    {
        public Guid DeviceId { get; protected set; }
        public Guid PolicyRuleId { get; protected set; }
        public int TenantId { get; set; }
        public DateTime OccurredAtUtc { get; protected set; }

        protected Violation() { }

        public Violation(Guid deviceId, Guid policyRuleId, int tenantId, DateTime occurredAtUtc)
        {
            DeviceId = deviceId;
            PolicyRuleId = policyRuleId;
            TenantId = tenantId;
            OccurredAtUtc = occurredAtUtc;
        }
    }
}
