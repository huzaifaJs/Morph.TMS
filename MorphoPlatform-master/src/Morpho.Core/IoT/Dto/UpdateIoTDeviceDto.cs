using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Morpho.Domain.Entities.IoT;

namespace Morpho.IoT.Dto
{
    [AutoMapTo(typeof(IoTDevice))]
    public class UpdateIoTDeviceDto
    {
        [Required]
        public Guid id { get; set; }

        [Required]
        [MaxLength(256)]
        public string name { get; set; }

        [Required]
        [MaxLength(128)]
        public string device_type { get; set; }
    }
}
