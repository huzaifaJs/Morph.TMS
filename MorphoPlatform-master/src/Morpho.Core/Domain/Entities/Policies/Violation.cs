using Abp.Domain.Entities.Auditing;
using System;
using Morpho.Domain.Enums;

namespace Morpho.Domain.Entities.Policies
{
    public class Violation : FullAuditedEntity<Guid>
    {
        public int TenantId { get; protected set; }

        public Guid DeviceId { get; protected set; }
        public Guid PolicyRuleId { get; protected set; }
       
        public DateTime OccurredAt { get; protected set; }
        public SensorType SensorType { get; protected set; }
        public decimal Value { get; protected set; }
        public string Status { get; protected set; }

        protected Violation() { }

        public Violation(int tenantId, Guid deviceId, Guid ruleId,
                         DateTime occurredAt, SensorType sensorType,
                         decimal value, string status)
        {
            TenantId = tenantId;
            DeviceId = deviceId;
            PolicyRuleId = ruleId;
            OccurredAt = occurredAt;
            SensorType = sensorType;
            Value = value;
            Status = status;
        }
    }
}
