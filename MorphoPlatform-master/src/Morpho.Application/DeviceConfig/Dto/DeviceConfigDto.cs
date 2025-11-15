using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using DeviceConfigEntity = Morpho.Domain.Entities.DeviceConfig;

namespace Morpho.Application.Configuration.DeviceConfig.Dto
{
    [AutoMapFrom(typeof(DeviceConfigEntity))]
    public class DeviceConfigDto : EntityDto<Guid>
    {
        public int tenant_id { get; set; }
        public Guid device_id { get; set; }

        public string configured_by { get; set; }
        public bool status { get; set; }

        public long frequency { get; set; }
        public int sf { get; set; }
        public int txp { get; set; }

        public string endpoint_url { get; set; }

        public bool debug_enable { get; set; }
        public bool sd_enable { get; set; }
        public bool rfid_enable { get; set; }

        public decimal? threshold_min { get; set; }
        public decimal? threshold_max { get; set; }
        public decimal? threshold_max_var { get; set; }
    }
}
