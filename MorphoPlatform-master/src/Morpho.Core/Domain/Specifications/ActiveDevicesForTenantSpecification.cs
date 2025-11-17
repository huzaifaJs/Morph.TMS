using Morpho.Domain.Entities.IoT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Specifications
{
    public class ActiveDevicesForTenantSpecification : Specification<IoTDevice>
    {
        private readonly int _tenantId;

        public ActiveDevicesForTenantSpecification(int tenantId)
        {
            _tenantId = tenantId;
        }

        public override Expression<Func<IoTDevice, bool>> Criteria =>
            device => device.TenantId == _tenantId && device.IsActive;
    }
}
