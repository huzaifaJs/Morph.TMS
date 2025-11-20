using Abp.Domain.Entities.Auditing;
using System;

namespace Morpho.Domain.Entities.IoT
{
    public class DeviceConfig : FullAuditedEntity<Guid>
    {
        public int TenantId { get; protected set; }
        public Guid DeviceId { get; protected set; }

        public string ConfiguredBy { get; protected set; }
        public bool RfidEnabled { get; protected set; }
        public bool SdEnabled { get; protected set; }
        public bool DebugEnabled { get; protected set; }
        public string EndpointUrl { get; protected set; }
        public decimal Frequency { get; protected set; }
        public string ThresholdJson { get; protected set; }

        protected DeviceConfig() { }

        public DeviceConfig(
            int tenantId,
            Guid deviceId,
            string configuredBy,
            bool rfidEnabled,
            bool sdEnabled,
            bool debugEnabled,
            string endpointUrl,
            decimal frequency,
            string thresholdJson)
        {
            TenantId = tenantId;
            DeviceId = deviceId;
            ConfiguredBy = configuredBy;
            RfidEnabled = rfidEnabled;
            SdEnabled = sdEnabled;
            DebugEnabled = debugEnabled;
            EndpointUrl = endpointUrl;
            Frequency = frequency;
            ThresholdJson = thresholdJson;
        }
    }
}
