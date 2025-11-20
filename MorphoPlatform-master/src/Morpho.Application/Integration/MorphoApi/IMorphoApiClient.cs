using Morpho.Integration.MorphoApi.Dto;
using System.Threading.Tasks;

namespace Morpho.Application.Integration.MorphoApi
{
    public interface IMorphoApiClient
    {
        // AUTH
        Task<string> GetAccessTokenAsync();

        // ===== GET APIs =====
        Task<DeviceStatusResponseDto> GetDeviceStatusAsync(int deviceId);
        Task<DeviceConfigResponseDto> GetDeviceConfigAsync(int deviceId);
        Task<DeviceConfigResponseDto> GetDeviceConfigResponseAsync(int deviceId);
        Task<DeviceRebootResponseDto> GetDeviceRebootResponseAsync(int deviceId);
        Task<DeviceLogsResponseDto> GetDeviceLogsAsync(int deviceId);
        Task<DeviceClearLogsCommandDto> GetDeviceClearLogsCommandAsync(int deviceId);
        Task<DeviceClearLogsResponseDto> GetDeviceClearLogsResponseAsync(int deviceId);

        // ===== POST APIs =====
        Task PostDeviceStatusAsync(DeviceStatusPushDto dto);
        Task PostDeviceConfigAsync(DeviceConfigPushDto dto);
        Task PostDeviceConfigResponseAsync(DeviceConfigResponseDto dto);
        Task PostDeviceRebootAsync(DeviceRebootPushDto dto);
        Task PostDeviceRebootResponseAsync(DeviceRebootResponseDto dto);
        Task PostDeviceLogsAsync(DeviceLogsPushDto dto);
        Task PostDeviceClearLogsAsync(DeviceClearLogsPushDto dto);

    }
}
