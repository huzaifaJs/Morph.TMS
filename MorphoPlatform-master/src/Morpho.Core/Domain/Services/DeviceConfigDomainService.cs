using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Morpho.Domain.Entities.IoT;

using Morpho.Integration.MorphoApi.Dto;

namespace Morpho.Domain.Services
{
    public class DeviceConfigDomainService
    {
        private readonly IRepository<DeviceConfig, Guid> _configRepository;

        public DeviceConfigDomainService(IRepository<DeviceConfig, Guid> configRepository)
        {
            _configRepository = configRepository;
        }

        public async Task UpdateFromMorphoAsync(IoTDevice device, DeviceConfigResponseDto dto)
        {
            var entity = new DeviceConfig(
                  device.TenantId,
                  device.Id,
                  "MorphoSync",
                  dto.RFID_enable,
                  dto.SD_enable,
                  dto.debug_enable,
                  dto.endpoint_URL,
                  dto.frequency,
                  dto.threshold_json
              );






            await _configRepository.InsertOrUpdateAsync(entity);

        }
    }
}
