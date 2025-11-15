using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Enums
{
    public enum SensorType
    {
        Unknown = 0,
        Temperature = 1,
        Humidity = 2,
        Light = 3,
        Shock = 4,
        Vibration = 5,
        Gps = 6,
        BatteryLevel = 7,
        Rssi = 8
    }
}
