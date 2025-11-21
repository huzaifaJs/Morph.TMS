using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Integration.MorphoApi.Dto
{
    public class DeviceLogsPushDto
    {
        public int device_id { get; set; }
        public string log { get; set; }
        public long timestamp { get; set; }
    }
}
