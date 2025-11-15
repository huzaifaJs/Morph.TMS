using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Morpho.Domain.Enums;
using System;

namespace Morpho.Domain.Entities.IoT
{
    public class IoTDevice : FullAuditedAggregateRoot<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        // Morpho raw device_id (string from external device)
        public string ExternalDeviceId { get; protected set; }
        public string SerialNumber { get; protected set; }
        public string Name { get; protected set; }
        public string DeviceType { get; protected set; }

        public DeviceStatusType Status { get; protected set; }
        public bool IsActive { get; protected set; }

        protected IoTDevice() { }

        public IoTDevice(
            int tenantId,
            string externalDeviceId,
            string serialNumber,
            string name,
            string deviceType)
        {
            TenantId = tenantId;
            ExternalDeviceId = externalDeviceId;
            SerialNumber = serialNumber;
            Name = name;
            DeviceType = deviceType;

            Status = DeviceStatusType.Unknown;
            IsActive = true;
        }

        public void UpdateStatus(DeviceStatusType status)
        {
            Status = status;
        }

        public void Deactivate() => IsActive = false;
    }
}
