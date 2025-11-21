using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using Morpho.Domain.Entities.IoT;

namespace Morpho.IoT.Dto
{
    [AutoMapTo(typeof(IoTDevice))]
    public class CreateIoTDeviceDto
    {
        [Required]
        public int tenant_id { get; set; }

        [Required]
        public int device_id { get; set; }

        [Required]
        [MaxLength(128)]
        public string serial_number { get; set; }

        [Required]
        [MaxLength(256)]
        public string name { get; set; }

        [Required]
        [MaxLength(128)]
        public string device_type { get; set; }
    }
}
