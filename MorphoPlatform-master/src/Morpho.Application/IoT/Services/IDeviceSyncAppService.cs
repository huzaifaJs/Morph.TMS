using System;
using System.Threading.Tasks;
using Morpho.Integration.MorphoApi.Dto;

namespace Morpho.Application.IoT.Services
{
    public interface IDeviceSyncAppService
    {
        Task<DeviceStatusResponseDto> SyncStatusAsync(Guid id);
        Task<DeviceConfigResponseDto> SyncConfigAsync(Guid id);
        Task<DeviceLogsResponseDto> SyncLogsAsync(Guid id);
    }
}
