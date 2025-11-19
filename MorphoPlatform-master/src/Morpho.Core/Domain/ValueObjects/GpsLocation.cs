using Abp.Domain.Values;
using System.Collections.Generic;

namespace Morpho.Domain.ValueObjects
{
    // EF Core: This is a Value Object, not an Entity.
    // MUST have a protected parameterless constructor.
    public class GpsLocation : ValueObject
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double? Altitude { get; private set; }
        public double? Accuracy { get; private set; }

        // Required by EF Core for owned types
        protected GpsLocation() { }

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
