using Abp.MultiTenancy;
using Morpho.Authorization.Users;
using Morpho.Domain.Entities;
using System.Collections.Generic;

namespace Morpho.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }

        public ICollection<TenantContact> Contacts { get; set; }
        public ICollection<TenantDocument> Documents { get; set; }
        public ICollection<TenantAddress> Addresses { get; set; }
    }
}
