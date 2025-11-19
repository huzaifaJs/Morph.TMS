using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Morpho.IoT.Dto;


namespace Morpho.Application.IoT
{
    public interface IIoTDeviceAppService : IApplicationService
    {
        Task<IoTDeviceDto> RegisterAsync(CreateIoTDeviceDto input);
        Task<IoTDeviceDto> GetAsync(Guid id);
        Task<PagedResultDto<IoTDeviceDto>> GetListAsync(GetIoTDevicesInputDto input);
        Task SyncStatusAsync(Guid id);
        Task SyncConfigAsync(Guid id);
        Task SyncLogsAsync(Guid id);
    }
}
