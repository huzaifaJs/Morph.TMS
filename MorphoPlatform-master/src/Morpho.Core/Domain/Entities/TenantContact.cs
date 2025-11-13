using Abp.Domain.Entities;
using Morpho.Domain.Common;
using Morpho.MultiTenancy;

namespace Morpho.Domain.Entities
{
 
    public class TenantContact : Entity
    {
        public int TenantId { get; set; }
        public Contact Contact { get; set; }

        #region Navigation Properties
        public Tenant Tenant { get; set; }
        #endregion
    }
}
