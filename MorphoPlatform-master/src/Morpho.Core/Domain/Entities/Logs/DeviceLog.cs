using Abp.Domain.Entities.Auditing;
using System;

namespace Morpho.Domain.Entities.Logs
{
    public class DeviceLog : FullAuditedEntity<Guid>
    {
        public int TenantId { get; protected set; }
        public Guid DeviceId { get; protected set; }
        public string Severity { get; protected set; }
        public string Message { get; protected set; }
        public DateTime TimestampUtc { get; protected set; }

        protected DeviceLog() { }

        public DeviceLog(
            int tenantId,
            Guid deviceId,
            string severity,
            string message,
            DateTime timestampUtc)
        {
            TenantId = tenantId;
            DeviceId = deviceId;
            Severity = severity;
            Message = message;
            TimestampUtc = timestampUtc;
        }
    }
}
