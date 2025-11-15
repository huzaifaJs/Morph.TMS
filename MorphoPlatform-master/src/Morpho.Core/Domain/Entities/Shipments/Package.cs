using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.Shipments
{
    public class Package : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid ContainerId { get; protected set; }
        public string Sku { get; protected set; }
        public int Quantity { get; protected set; }

        public Guid? PolicyId { get; protected set; }

        protected Package() { }

        public Package(int tenantId, Guid containerId, string sku, int quantity)
        {
            TenantId = tenantId;
            ContainerId = containerId;
            Sku = sku;
            Quantity = quantity;
        }
    }
}
