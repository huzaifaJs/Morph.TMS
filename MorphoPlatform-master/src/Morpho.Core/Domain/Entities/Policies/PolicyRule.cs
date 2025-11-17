using Abp.Domain.Entities.Auditing;
using Morpho.Domain.Enums;
using Morpho.Domain.ValueObjects;
using System;

namespace Morpho.Domain.Entities.Policies
{
    public class PolicyRule : FullAuditedEntity<Guid>
    {
        public int TenantId { get; set; }

        // Parent
        public Guid PolicyId { get; set; }
        public virtual Policy Policy { get; set; }

        // Sensor category (Temperature, Humidity, etc.)
        public SensorType SensorType { get; protected set; }

        // <, >, Range
        public PolicyConditionType ConditionType { get; protected set; }

        // Thresholds (Min/Max)
        public ThresholdRange Threshold { get; protected set; }



        protected PolicyRule() { }

        public PolicyRule(
            int tenantId,
            Guid policyId,
            SensorType sensorType,
            PolicyConditionType conditionType,
            ThresholdRange threshold)
        {
            TenantId = tenantId;
            PolicyId = policyId;
            SensorType = sensorType;
            ConditionType = conditionType;
            Threshold = threshold;
        }
    }
}
