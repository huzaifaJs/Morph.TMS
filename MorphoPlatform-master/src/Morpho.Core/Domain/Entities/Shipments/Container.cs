using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.Shipments
{
    public class Container : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid ShipmentId { get; protected set; }
        public string ContainerIdentifier { get; protected set; }

        public decimal? Weight { get; protected set; }
        public string ContainerType { get; protected set; }

        public Guid? PolicyId { get; protected set; }

        public virtual ICollection<Package> Packages { get; protected set; }

        protected Container() { }

        public Container(int tenantId, Guid shipmentId, string containerIdentifier)
        {
            TenantId = tenantId;
            ShipmentId = shipmentId;
            ContainerIdentifier = containerIdentifier;
            Packages = new List<Package>();
        }
    }
}
