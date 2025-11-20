using Abp.Application.Services;
using Morpho.Device.DeviceTypeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.DeviceType
{
    public interface IDeviceTypeAppService: IApplicationService
    {
        Task<List<DeviceTypeDto>> GetDeviceTypesListAsync();
        Task<CreateDeviceTypeDto> AddDeviceTypeAsync(CreateDeviceTypeDto input);
        Task<UpdateDeviceTypeDto> UpdateDeviceTypeAsync(UpdateDeviceTypeDto input);
        Task<UpdateStatusDeviceTypeDto> UpdateDeviceTypeStatusAsync(UpdateStatusDeviceTypeDto input);
        Task<DeviceTypeDto> GetDeviceTypeDetailsAsync(long deviceTypeId);
        Task<UpdateStatusDeviceTypeDto> DeleteDeviceTypeAsync(UpdateStatusDeviceTypeDto input);
    }
}
