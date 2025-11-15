using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.Shipments
{
    public class ShipmentDeviceAssignment : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid ShipmentId { get; protected set; }
        public Guid DeviceId { get; protected set; }

        public string TargetType { get; protected set; }   // Shipment / Container / Package
        public Guid? TargetId { get; protected set; }

        protected ShipmentDeviceAssignment() { }

        public ShipmentDeviceAssignment(int tenantId, Guid shipmentId, Guid deviceId, string targetType, Guid? targetId)
        {
            TenantId = tenantId;
            ShipmentId = shipmentId;
            DeviceId = deviceId;
            TargetType = targetType;
            TargetId = targetId;
        }
    }
}
