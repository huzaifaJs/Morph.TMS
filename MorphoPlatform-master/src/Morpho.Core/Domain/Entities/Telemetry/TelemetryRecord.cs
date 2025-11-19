using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Morpho.Domain.Enums;
using Morpho.Domain.ValueObjects;
using System;

namespace Morpho.Domain.Entities.Telemetry
{
    public class TelemetryRecord : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid DeviceId { get; protected set; }
        public Guid? ShipmentId { get; protected set; }
        public Guid? ContainerId { get; protected set; }

        public DateTime Timestamp { get; protected set; }

        public SensorType SensorType { get; protected set; }
        public decimal Value { get; protected set; }
        public string Unit { get; protected set; }

        public GpsLocation Gps { get; protected set; }
        public decimal? BatteryLevel { get; protected set; }

        protected TelemetryRecord() { }

        public TelemetryRecord(
            int tenantId,
            Guid deviceId,
            SensorType sensorType,
            decimal value,
            string unit,
            DateTime timestamp,
            GpsLocation gps = null,
            decimal? batteryLevel = null)
        {
            TenantId = tenantId;
            DeviceId = deviceId;
            SensorType = sensorType;
            Value = value;
            Unit = unit;
            Timestamp = timestamp;
            Gps = gps;
            BatteryLevel = batteryLevel;
        }

        public void LinkShipment(Guid shipmentId, Guid? containerId)
        {
            ShipmentId = shipmentId;
            ContainerId = containerId;
        }
    }
}
