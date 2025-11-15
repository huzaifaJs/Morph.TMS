using Morpho.Domain.Entities.IoT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Interfaces
{
    public interface IIoTDeviceService
    {
        Task<IoTDevice> GetByExternalIdAsync(int tenantId, string externalDeviceId);
        Task<IoTDevice> RegisterOrUpdateAsync(int tenantId, string externalDeviceId, string serialNumber, string name, string deviceType);
    }
}
