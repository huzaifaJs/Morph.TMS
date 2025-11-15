using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.IoT
{
    public class DeviceConfig : AuditedAggregateRoot<Guid>
    {
        public Guid DeviceId { get; set; }
        public string ConfiguredBy { get; set; } = string.Empty;
        public bool RfidEnabled { get; set; }
        public bool SdEnabled { get; set; }
        public bool DebugEnabled { get; set; }
        public string EndpointUrl { get; set; } = string.Empty;
        public decimal Frequency { get; set; }
        public string ThresholdJson { get; set; } = string.Empty; // store as JSON
    }
}
