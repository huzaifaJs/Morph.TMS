using Morpho.Domain.Common;
using Morpho.MultiTenancy;
using Morpho.Domain.Enums;
using Abp.Domain.Entities;

namespace Morpho.Domain.Entities
{
    public class TenantAddress : Entity
    {
        public int TenantId { get; set; }
        public TenantAddressType Type { get; set; }
        public Address Address { get; set; }

        #region Navigation Properties
        public Tenant Tenant { get; set; }
        #endregion
    }
}
