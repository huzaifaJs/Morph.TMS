using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Integration.MorphoApi.Dto
{
    public class MorphoTelemetryPushDto
    {
        public int device_id { get; set; }
        public int client_id { get; set; }
        public string firmware_version { get; set; }
        public string ip_address { get; set; }
        public long timestamp { get; set; }

        public GpsInfo gps { get; set; }

        public double rssi { get; set; }
        public double battery_level { get; set; }
        public double temperature { get; set; }
        public double humidity { get; set; }
        public double mean_vibration { get; set; }
        public double light { get; set; }
        public string status { get; set; }
        public int nbrfid { get; set; }
    }
}
