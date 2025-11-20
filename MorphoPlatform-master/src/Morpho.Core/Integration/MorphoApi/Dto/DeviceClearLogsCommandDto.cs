using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Integration.MorphoApi.Dto
{
    public class DeviceClearLogsCommandDto
    {
        public int device_id { get; set; }
        public bool clear { get; set; }
    }
}
