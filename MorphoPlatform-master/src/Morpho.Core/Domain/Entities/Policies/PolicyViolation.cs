using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Morpho.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.Policies
{
    public class PolicyViolation : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid PolicyId { get; protected set; }
        public Guid PolicyRuleId { get; protected set; }

        public Guid DeviceId { get; protected set; }
        public Guid? ShipmentId { get; protected set; }
        public Guid? ContainerId { get; protected set; }

        public SensorType SensorType { get; protected set; }
        public decimal MeasuredValue { get; protected set; }
        public string Unit { get; protected set; }

        public DateTime OccurredAt { get; protected set; }
        public bool Acknowledged { get; protected set; }

        protected PolicyViolation() { }

        public PolicyViolation(
            int tenantId,
            Guid policyId,
            Guid policyRuleId,
            Guid deviceId,
            SensorType sensorType,
            decimal measuredValue,
            string unit,
            DateTime occurredAt,
            Guid? shipmentId = null,
            Guid? containerId = null)
        {
            TenantId = tenantId;
            PolicyId = policyId;
            PolicyRuleId = policyRuleId;
            DeviceId = deviceId;
            SensorType = sensorType;
            MeasuredValue = measuredValue;
            Unit = unit;
            OccurredAt = occurredAt;
            ShipmentId = shipmentId;
            ContainerId = containerId;
        }

        public void Acknowledge() => Acknowledged = true;
    }
}
