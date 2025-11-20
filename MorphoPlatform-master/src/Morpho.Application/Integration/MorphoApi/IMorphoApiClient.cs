using Abp.Application.Services;
using Morpho.Integration.MorphoApi.Dto;
using System.Threading.Tasks;


namespace Morpho.Application.Integration.MorphoApi
{
    public interface IMorphoApiClient: IApplicationService
    {
        // Authentication
        Task<string> GetAccessTokenAsync();

        // GET calls (Morpho → Our system)
        Task<DeviceStatusResponseDto> GetDeviceStatusAsync(string externalDeviceId);
        Task<DeviceConfigResponseDto> GetDeviceConfigAsync(string externalDeviceId);
        Task<DeviceLogsResponseDto> GetDeviceLogsAsync(string externalDeviceId);

        // POST calls (Our system → Morpho)
        Task SetDeviceConfigAsync(DeviceConfigPushDto dto);
        Task SetDeviceRebootAsync(DeviceRebootPushDto dto);
        Task ClearDeviceLogsAsync(DeviceClearLogsPushDto dto);
    }
}
