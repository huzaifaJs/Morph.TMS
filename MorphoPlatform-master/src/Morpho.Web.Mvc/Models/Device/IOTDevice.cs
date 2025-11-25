using Morpho.Device.TrackingDeviceDto;
using Morpho.Roles.Dto;
using Morpho.Users.Dto;
using Morpho.VehicleType.Dto;
using System.Collections.Generic;

namespace Morpho.Web.Models.Device
{
    public class IOTDeviceViewModel
    {
        public CreateDeviceDto CreateDeviceDto { get; set; }

        public UpdateDeviceDto UpdateDeviceDto { get; set; }

        // For Listing (optional)
        public IReadOnlyList<DeviceDto> DeviceDto { get; set; }

        public IOTDeviceViewModel()
        {
            CreateDeviceDto = new CreateDeviceDto();
            UpdateDeviceDto = new UpdateDeviceDto();
            DeviceDto = new List<DeviceDto>();
        }

        public IReadOnlyList<DeviceDto> LstDevice { get; set; }
            = new List<DeviceDto>();
    }

}