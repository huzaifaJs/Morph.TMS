using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Morpho.EntityFrameworkCore.Repositories
{
    public class IoTDeviceRepository
        : MorphoRepositoryBase<IoTDevice, Guid>, IIoTDeviceRepository
    {
        public IoTDeviceRepository(IDbContextProvider<MorphoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// Returns the device or throws exception if not found.
        /// </summary>
        public async Task<IoTDevice> GetByMorphoDeviceIdAsync(string morphoDeviceId)
        {
            var device = await GetAll()
                .FirstOrDefaultAsync(d => d.ExternalDeviceId == morphoDeviceId);

            if (device == null)
            {
                throw new Exception($"IoTDevice not found for external device id: {morphoDeviceId}");
            }

            return device;
        }

        /// <summary>
        /// Returns null if not found.
        /// </summary>
        public async Task<IoTDevice> FirstOrDefaultByMorphoDeviceIdAsync(string morphoDeviceId)
        {
            return await GetAll()
                .FirstOrDefaultAsync(d => d.ExternalDeviceId == morphoDeviceId);
        }

        /// <summary>
        /// Unique composite lookup: ExternalDeviceId + TenantId.
        /// </summary>
        public async Task<IoTDevice> GetByExternalIdAsync(string externalDeviceId, int tenantId)
        {
            return await GetAll()
                .FirstOrDefaultAsync(d =>
                    d.ExternalDeviceId == externalDeviceId &&
                    d.TenantId == tenantId);
        }
    }
}
