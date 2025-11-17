using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Morpho.Telemetry.Dto
{
    [AutoMapTo(typeof(Morpho.Domain.Entities.Telemetry.TelemetryRecord))]
    public class DeviceStatusInputDto
    {
        [Required]
        public int tenant_id { get; set; }

        [Required]
        [MaxLength(64)]
        public string external_device_id { get; set; }

        public Guid? device_id { get; set; }

        public int? morpho_device_id { get; set; }

        [Required]
        public long timestamp { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public double accuracy { get; set; }

        public double temperature { get; set; }
        public double humidity { get; set; }
        public double mean_vibration { get; set; }
        public double light { get; set; }
        public double battery_level { get; set; }
        public int rssi { get; set; }
    }
}
