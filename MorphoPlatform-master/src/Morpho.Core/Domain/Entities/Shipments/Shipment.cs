using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Morpho.Domain.Enums;

namespace Morpho.Domain.Entities.Shipments
{
    public class Shipment : FullAuditedAggregateRoot<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public string ShipmentNumber { get; protected set; }
        public ShipmentStatus Status { get; protected set; }

        public Guid? SourceLocationId { get; protected set; }
        public Guid? DestinationLocationId { get; protected set; }

        public DateTime? ExpectedDispatchDate { get; protected set; }
        public DateTime? ExpectedDeliveryDate { get; protected set; }

        public virtual ICollection<Container> Containers { get; protected set; }

        protected Shipment() { }

        public Shipment(int tenantId, string shipmentNumber)
        {
            TenantId = tenantId;
            ShipmentNumber = shipmentNumber;
            Status = ShipmentStatus.Created;
            Containers = new List<Container>();
        }

        public void SetStatus(ShipmentStatus status) => Status = status;
    }
}
