using Abp.Domain.Entities.Auditing;
using Abp.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.Telemetry
{
    public class Telemetry : ValueObject
    {
        public double Temperature { get; private set; }
        public double Humidity { get; private set; }
        public double Light { get; private set; }
        public double Vibration { get; private set; }
        public int Battery { get; private set; }
        public int Rssi { get; private set; }

        public DateTime Timestamp { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Temperature;
            yield return Humidity;
            yield return Light;
            yield return Vibration;
            yield return Battery;
            yield return Rssi;
            yield return Timestamp;
        }
    }

}
