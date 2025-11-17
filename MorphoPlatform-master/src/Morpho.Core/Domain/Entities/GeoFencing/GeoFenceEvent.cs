using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Morpho.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.GeoFencing
{
    public class GeoFenceEvent : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid GeoFenceZoneId { get; protected set; }
        public Guid DeviceId { get; protected set; }
        public Guid? ShipmentId { get; protected set; }

        public GeoFenceEventType EventType { get; protected set; }
        public DateTime Timestamp { get; protected set; }

        public double Latitude { get; protected set; }
        public double Longitude { get; protected set; }

        protected GeoFenceEvent() { }

        public GeoFenceEvent(
            int tenantId,
            Guid geoFenceZoneId,
            Guid deviceId,
            GeoFenceEventType eventType,
            DateTime timestamp,
            double latitude,
            double longitude,
            Guid? shipmentId = null)
        {
            TenantId = tenantId;
            GeoFenceZoneId = geoFenceZoneId;
            DeviceId = deviceId;
            EventType = eventType;
            Timestamp = timestamp;
            Latitude = latitude;
            Longitude = longitude;
            ShipmentId = shipmentId;
        }
    }
}
