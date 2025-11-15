using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using DeviceConfigEntity = Morpho.Domain.Entities.DeviceConfig;

namespace Morpho.Application.Configuration.DeviceConfig.Dto
{
    [AutoMapTo(typeof(DeviceConfigEntity))]
    public class CreateDeviceConfigDto
    {
        [Required]
        public int tenant_id { get; set; }

        [Required]
        public Guid device_id { get; set; }

        [Required]
        [MaxLength(128)]
        public string configured_by { get; set; }

        public bool status { get; set; }

        public long frequency { get; set; }
        public int sf { get; set; }
        public int txp { get; set; }

        [MaxLength(300)]
        public string endpoint_url { get; set; }

        public bool debug_enable { get; set; }
        public bool sd_enable { get; set; }
        public bool rfid_enable { get; set; }

        // threshold JSON stored as normalized rule entities
        public decimal? threshold_min { get; set; }
        public decimal? threshold_max { get; set; }
        public decimal? threshold_max_var { get; set; }
    }
}
