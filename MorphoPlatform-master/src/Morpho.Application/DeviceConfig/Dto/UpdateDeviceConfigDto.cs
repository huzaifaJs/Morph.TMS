using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using DeviceConfigEntity = Morpho.Domain.Entities.DeviceConfig;

namespace Morpho.Application.Configuration.DeviceConfig.Dto
{
    [AutoMapTo(typeof(DeviceConfigEntity))]
    public class UpdateDeviceConfigDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int tenant_id { get; set; }

        [Required]
        [MaxLength(128)]
        public string configured_by { get; set; }

        public bool status { get; set; }
        public long frequency { get; set; }

        public int sf { get; set; }
        public int txp { get; set; }

        public bool debug_enable { get; set; }
        public bool sd_enable { get; set; }
        public bool rfid_enable { get; set; }

        public decimal? threshold_min { get; set; }
        public decimal? threshold_max { get; set; }
        public decimal? threshold_max_var { get; set; }
    }
}
