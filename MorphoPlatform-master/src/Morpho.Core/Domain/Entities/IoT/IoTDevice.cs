using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Morpho.Domain.Entities.Policies;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Enums;
using Morpho.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Morpho.Domain.Entities.IoT
{
    public class IoTDevice : FullAuditedAggregateRoot<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        // NEW — The actual integer device_id coming from Morpho API
        public int MorphoDeviceId { get; protected set; }

        public string SerialNumber { get; protected set; }
        public string Name { get; protected set; }
        public string DeviceType { get; protected set; }

        public DeviceStatusType Status { get; protected set; }
        public bool IsActive { get; protected set; }

        // Owned value object for GPS
        public GpsLocation LastKnownLocation { get; protected set; }

        // Navigation collections
        public ICollection<TelemetryRecord> TelemetryRecords { get; protected set; }
            = new List<TelemetryRecord>();

        public ICollection<Violation> Violations { get; protected set; }
            = new List<Violation>();

        // EF Core requires a protected parameterless constructor
        protected IoTDevice() { }

        public IoTDevice(
            int tenantId,
            int morphoDeviceId,
            string serialNumber,
            string name,
            string deviceType)
        {
            TenantId = tenantId;
            MorphoDeviceId = morphoDeviceId;
            SerialNumber = serialNumber;
            Name = name;
            DeviceType = deviceType;

            Status = DeviceStatusType.Unknown;
            IsActive = true;
        }

        // Domain Behaviors
        public void UpdateStatus(DeviceStatusType status)
        {
            Status = status;
        }

        public void SetLastKnownLocation(double lat, double lng)
        {
            LastKnownLocation = new GpsLocation(lat, lng);
        }

        public void UpdateLastKnownLocation(GpsLocation gps)
        {
            LastKnownLocation = gps;
        }

        public void Deactivate() => IsActive = false;
    }
}
