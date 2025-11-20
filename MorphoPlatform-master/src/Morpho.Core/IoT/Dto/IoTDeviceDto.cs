using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.IoT;
using System;

namespace Morpho.IoT.Dto
{
    [AutoMapFrom(typeof(IoTDevice))]
    public class IoTDeviceDto : EntityDto<Guid>
    {
        public int tenant_id { get; set; }

        public string external_device_id { get; set; }
        public string serial_number { get; set; }
        public string name { get; set; }
        public string device_type { get; set; }

        public bool is_active { get; set; }
        public int status { get; set; } // DeviceStatusType enum
    }
}
