using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Morpho.Domain.ValueObjects;
using System;

namespace Morpho.Domain.Entities
{
    public class DeviceConfig : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Guid DeviceId { get; protected set; }
        public string ConfiguredBy { get; protected set; }

        // Value object
        public ThresholdRange Thresholds { get; protected set; }

        protected DeviceConfig() { }

        public DeviceConfig(int tenantId, Guid deviceId, string configuredBy, ThresholdRange thresholds)
        {
            TenantId = tenantId;
            DeviceId = deviceId;
            ConfiguredBy = configuredBy;
            Thresholds = thresholds;
        }
    }
}
