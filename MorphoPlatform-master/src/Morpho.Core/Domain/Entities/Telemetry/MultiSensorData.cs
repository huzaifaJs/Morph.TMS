using Abp.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.Telemetry
{
    public class MultiSensorData : ValueObject
    {
        public SensorReadings Readings { get; private set; }
        public DateTime Timestamp { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Readings;
            yield return Timestamp;
        }
    }

}
