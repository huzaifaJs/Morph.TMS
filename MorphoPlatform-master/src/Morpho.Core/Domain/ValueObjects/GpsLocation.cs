using Abp.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.ValueObjects
{
    public class GpsLocation : ValueObject
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double? Altitude { get; private set; }
        public double? Accuracy { get; private set; }

        public GpsLocation(double latitude, double longitude, double? altitude = null, double? accuracy = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            Accuracy = accuracy;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Latitude;
            yield return Longitude;
            yield return Altitude;
            yield return Accuracy;
        }
    }
}
