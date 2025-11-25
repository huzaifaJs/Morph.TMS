using Abp.Domain.Entities.Auditing;
using Morpho.Domain.Enums;
using System;

public class PolicyViolation : FullAuditedEntity<Guid>
{
    public int TenantId { get; protected set; }
    public Guid PolicyId { get; protected set; }
    public Guid RuleId { get; protected set; }
    public Guid DeviceId { get; protected set; }

    public SensorType SensorType { get; protected set; }
    public decimal Value { get; protected set; }
    public string Unit { get; protected set; }

    public DateTime OccurredAtUtc { get; protected set; }

    // Shipment relationship
    public Guid? ShipmentId { get; protected set; }
    public Guid? ContainerId { get; protected set; }
    protected PolicyViolation() { }
    public PolicyViolation(
                int tenantId,
                Guid policyId,
                Guid ruleId,
                Guid deviceId,
                SensorType sensorType,
                decimal value,
                string unit,
                DateTime occurredAtUtc,
                Guid? shipmentId,
                Guid? containerId)
    {
        TenantId = tenantId;
        PolicyId = policyId;
        RuleId = ruleId;
        DeviceId = deviceId;

        SensorType = sensorType;
        Value = value;
        Unit = unit;

        OccurredAtUtc = occurredAtUtc;

        ShipmentId = shipmentId;
        ContainerId = containerId;
    }
}
