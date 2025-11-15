using System;
namespace Morpho.Telemetry.Dto
{
    public class DeviceStatusInputDto
    {
        public int TenantId { get; set; }

        public string ExternalDeviceId { get; set; }   // Morpho device_id string
        public Guid DeviceId { get; set; }             // Internal GUID
        public int MorphoDeviceId { get; set; }        // optional numeric id

        public long Timestamp { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Accuracy { get; set; }

        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double MeanVibration { get; set; }
        public double Light { get; set; }
        public double BatteryLevel { get; set; }
        public int Rssi { get; set; }
    }
}

