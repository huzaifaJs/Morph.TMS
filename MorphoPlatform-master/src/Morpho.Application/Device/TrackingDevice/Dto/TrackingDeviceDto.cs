using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Device.TrackingDeviceDto
{
    [AutoMapTo(typeof(TrackingDevices))]
    public class CreateDeviceDto
    {
        [Required]
        public string device_type_name { get; set; }
        [Required]
        public string device_unique_no { get; set; }
        public string device_type { get; set; }
        [Required]
        public string device_name { get; set; }
        public string manufacturer { get; set; }
        public string remark { get; set; }
        [Required]
        public string serial_number { get; set; }
        [Required]
        public string imei_number { get; set; }
        public decimal min_value { get; set; }
        public decimal? max_value { get; set; }
    }

    [AutoMapTo(typeof(TrackingDevices))]
    public class UpdateDeviceDto
    {
        [Required]
        public long Id { get; set; }
        public string device_type_name { get; set; }
        public string device_unique_no { get; set; }
        public string device_type { get; set; }
        public string device_name { get; set; }
        public string manufacturer { get; set; }
        public string remark { get; set; }
        public string serial_number { get; set; }
        public string imei_number { get; set; }
        public decimal min_value { get; set; }
        public decimal? max_value { get; set; }
    }
    [AutoMapTo(typeof(TrackingDevices))]
    public class UpdateStatusDeviceDto
    {
        [Required]
        public long Id { get; set; }
    }

    [AutoMapFrom(typeof(TrackingDevices))]
    public class DeviceDto : EntityDto<long>
    {
        public string device_type { get; set; }
        public string device_name { get; set; }
        public string manufacturer { get; set; }
        public string model_no { get; set; }
        public string remark { get; set; }
        public string serial_number { get; set; }
        public string imei_number { get; set; }
        public decimal min_value { get; set; }
        public decimal? max_value { get; set; }
        public DateTime Created_At { get; set; }
        public long? Created_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public long? Updated_By { get; set; }
    }

    public class DevicePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
