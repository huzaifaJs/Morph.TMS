using Abp.Events.Bus;
using Morpho.Domain.Entities.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Events
{
    public class TelemetryReceivedEvent : EventData
    {
        public TelemetryRecord Telemetry { get; }

        public TelemetryReceivedEvent(TelemetryRecord telemetry)
        {
            Telemetry = telemetry;
        }
    }
}
