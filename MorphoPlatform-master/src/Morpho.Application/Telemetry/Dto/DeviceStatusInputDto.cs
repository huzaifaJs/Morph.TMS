using System;
using System.ComponentModel.DataAnnotations;

namespace Morpho.Integration.MorphoApi.Dto
{
    public class DeviceStatusInputDto
    {
        // ============================
        // Tenant + Device Identity
        // ============================

        [Required]
        public int tenant_id { get; set; }

        // External ID from device (the real device_id in client's system)
        [Required]
        [MaxLength(64)]
        public string external_device_id { get; set; }

        // Our internal IoTDevice GUID (optional)
        public Guid? device_id { get; set; }

        // Integer device_id used by Morpho platform
        public int? morpho_device_id { get; set; }

        // ============================
        // Timestamp
        // ============================

        [Required]
        public long timestamp { get; set; }   // UNIX time

        // ============================
        // GPS Fields
        // ============================

        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public double accuracy { get; set; }

        // ============================
        // Telemetry Values
        // ============================

        public double temperature { get; set; }
        public double humidity { get; set; }

        [Display(Name = "mean_vibration")]
        public double mean_vibration { get; set; }

        public double light { get; set; }
        public double battery_level { get; set; }

        public int rssi { get; set; }

        // ============================
        // Missing from previous DTO — NOW ADDED
        // ============================

        [MaxLength(64)]
        public string status { get; set; }

        public int nbrfid { get; set; }
    }
}
