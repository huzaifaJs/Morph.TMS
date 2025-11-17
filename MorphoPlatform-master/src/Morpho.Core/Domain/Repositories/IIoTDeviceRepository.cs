using Abp.Domain.Repositories;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.IoT;
using System;
using System.Threading.Tasks;

namespace Morpho.Domain.Repositories
{
    public interface IIoTDeviceRepository : IRepository<IoTDevice, Guid>
    {
        /// <summary>
        /// Find a device by its Morpho Device ID (string like "7777")
        /// </summary>
        Task<IoTDevice> GetByMorphoDeviceIdAsync(string morphoDeviceId);

        /// <summary>
        /// Returns null if not found
        /// </summary>
        Task<IoTDevice> FirstOrDefaultByMorphoDeviceIdAsync(string morphoDeviceId);

        Task<IoTDevice> GetByExternalIdAsync(string externalDeviceId, int tenantId);
    }
}
