using Abp.Application.Services;
using Morpho.Device.DeviceTypeDto;
using Morpho.Device.TrackingDeviceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Device.TrackingDevice
{
    public interface IDeviceManagementAppService : IApplicationService
    {
        Task<List<DeviceDto>> GetDeviceListAsync();
        Task<CreateDeviceDto> AddDeviceAsync(CreateDeviceDto input);
        Task<UpdateDeviceDto> UpdateDeviceAsync(UpdateDeviceDto input);
        Task<UpdateStatusDeviceDto> UpdateDeviceStatusAsync(UpdateStatusDeviceDto input);
        Task<DeviceDto> GetDeviceDetailsAsync(long deviceTypeId);
        Task<UpdateStatusDeviceDto> DeleteDeviceAsync(UpdateStatusDeviceDto input);
    }
}
