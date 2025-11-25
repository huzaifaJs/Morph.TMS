using System;
using Abp.Domain.Entities.Auditing;

namespace Morpho.Domain.Entities.Events
{
    public class DeviceEvent : FullAuditedEntity<Guid>
    {
        public int TenantId { get; protected set; }
        public Guid DeviceId { get; protected set; }

        public string EventType { get; protected set; }
        public string Message { get; protected set; }
        public DateTime TimestampUtc { get; protected set; }
       
        protected DeviceEvent() { }

        public DeviceEvent(
            int tenantId,
            Guid deviceId,
            string eventType,
            string message,
            DateTime timestampUtc)
        {
            TenantId = tenantId;
            DeviceId = deviceId;
            EventType = eventType;
            Message = message;
            TimestampUtc = timestampUtc;
        }
    }
}
