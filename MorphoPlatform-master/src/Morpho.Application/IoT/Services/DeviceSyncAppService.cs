using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Morpho.Application.Integration.MorphoApi;
using Morpho.Domain.Entities.IoT;
using Morpho.Integration.MorphoApi;
using Morpho.Integration.MorphoApi.Dto;
using Morpho.IoT.Dto;
using System;
using System.Threading.Tasks;

namespace Morpho.Application.IoT.Services
{
    [AbpAuthorize]
    public class DeviceSyncAppService : ApplicationService
    {
        private readonly IRepository<IoTDevice, Guid> _deviceRepository;
        private readonly IMorphoApiClient _morphoApiClient;

        public DeviceSyncAppService(
            IRepository<IoTDevice, Guid> deviceRepository,
            IMorphoApiClient morphoApiClient)
        {
            _deviceRepository = deviceRepository;
            _morphoApiClient = morphoApiClient;
        }

        // ------------------------------------------------------------
        // 1. SYNC STATUS
        // ------------------------------------------------------------
        public async Task<DeviceStatusResponseDto> SyncStatusAsync(Guid id)
        {
            var device = await _deviceRepository.FirstOrDefaultAsync(id)
                ?? throw new UserFriendlyException("Device not found.");

            if (device.MorphoDeviceId <= 0)
                throw new UserFriendlyException("Morpho Device ID missing.");

            var result = await _morphoApiClient.GetDeviceStatusAsync(device.MorphoDeviceId);

            // Update device status
            if (result != null)
            {
                var statusEnum = result.status.ToDeviceStatusType();
                device.UpdateStatus(statusEnum);
                await _deviceRepository.UpdateAsync(device);
            }

            return result;
        }



        // ------------------------------------------------------------
        // 2. SYNC CONFIG
        // ------------------------------------------------------------
        public async Task<DeviceConfigResponseDto> SyncConfigAsync(Guid id)
        {
            var device = await _deviceRepository.FirstOrDefaultAsync(id)
                ?? throw new UserFriendlyException("Device not found.");

            if (device.MorphoDeviceId<0)
                throw new UserFriendlyException("Morpho Device ID missing.");

            var result = await _morphoApiClient.GetDeviceConfigAsync(device.MorphoDeviceId);

            return result;
        }

        // ------------------------------------------------------------
        // 3. SYNC LOGS
        // ------------------------------------------------------------
        public async Task<DeviceLogsResponseDto> SyncLogsAsync(Guid id)
        {
            var device = await _deviceRepository.FirstOrDefaultAsync(id)
                ?? throw new UserFriendlyException("Device not found.");

            if (device.MorphoDeviceId <= 0)
                throw new UserFriendlyException("Morpho Device ID missing or invalid.");

            var result = await _morphoApiClient.GetDeviceLogsAsync(device.MorphoDeviceId);

            return result;
        }


        // ------------------------------------------------------------
        // 4. REBOOT DEVICE
        // ------------------------------------------------------------
        public async Task<bool> RebootAsync(Guid id)
        {
            var device = await _deviceRepository.FirstOrDefaultAsync(id)
                ?? throw new UserFriendlyException("Device not found.");

            if (device.MorphoDeviceId <= 0)
                throw new UserFriendlyException("Morpho Device ID missing.");

            var dto = new DeviceRebootPushDto
            {
                device_id = device.MorphoDeviceId
            };

            await _morphoApiClient.PostDeviceRebootAsync(dto);

            return true;
        }

        // ------------------------------------------------------------
        // 5. CLEAR LOGS
        // ------------------------------------------------------------
        public async Task<bool> ClearLogsAsync(Guid id)
        {
            var device = await _deviceRepository.FirstOrDefaultAsync(id)
                ?? throw new UserFriendlyException("Device not found.");

            if (device.MorphoDeviceId <= 0)
                throw new UserFriendlyException("Morpho Device ID missing.");

            var dto = new DeviceClearLogsPushDto
            {
                device_id = device.MorphoDeviceId
            };

            await _morphoApiClient.PostDeviceClearLogsAsync(dto);

            return true;
        }
    }
}
