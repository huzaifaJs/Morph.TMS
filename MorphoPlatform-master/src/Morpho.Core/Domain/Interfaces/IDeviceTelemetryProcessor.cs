using Morpho.Domain.Entities.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Interfaces
{
    public interface IDeviceTelemetryProcessor
    {
        Task ProcessTelemetryAsync(TelemetryRecord telemetry);
    }
}
