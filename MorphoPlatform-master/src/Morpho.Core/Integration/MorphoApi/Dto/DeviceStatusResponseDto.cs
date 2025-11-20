using System.Collections.Generic;

namespace Morpho.Integration.MorphoApi.Dto

{
    public class DeviceStatusResponseDto
    {
        public int device_id { get; set; }
        public int client_id { get; set; }
        public string firmware_version { get; set; }
        public string ip_address { get; set; }
        public long timestamp { get; set; }
        public GpsDto gps { get; set; }
        public int rssi { get; set; }
        public double batterie_level { get; set; }
        public double temperature { get; set; }
        public double humidity { get; set; }
        public double mean_vibration { get; set; }
        public double light { get; set; }
        public string status { get; set; }
    }

    public class GpsDto
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public double accuracy { get; set; }
    }
}
