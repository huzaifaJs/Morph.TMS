using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Integration.MorphoApi.Dto
{
    public class DeviceClearLogsResponseDto
    {
        public int device_id { get; set; }
        public bool cleared { get; set; }
        public string message { get; set; }
    }
}
