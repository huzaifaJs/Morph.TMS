using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Enums
{
    public class ShipmentTypeStatus
    {
        public string Value { get; private set; }

        private ShipmentTypeStatus(string value)
        {
            Value = value;
        }

        public static readonly ShipmentTypeStatus Pending = new ShipmentTypeStatus("Pending");
        public static readonly ShipmentTypeStatus Processing = new ShipmentTypeStatus("Processing");
        public static readonly ShipmentTypeStatus ReadyForPickup = new ShipmentTypeStatus("Ready for Pickup");
        public static readonly ShipmentTypeStatus Shipped = new ShipmentTypeStatus("Shipped");
        public static readonly ShipmentTypeStatus InTransit = new ShipmentTypeStatus("In Transit");
        public static readonly ShipmentTypeStatus OutForDelivery = new ShipmentTypeStatus("Out for Delivery");
        public static readonly ShipmentTypeStatus Delivered = new ShipmentTypeStatus("Delivered");
        public static readonly ShipmentTypeStatus DeliveryFailed = new ShipmentTypeStatus("Delivery Failed");
        public static readonly ShipmentTypeStatus ReturnedToOrigin = new ShipmentTypeStatus("Returned to Origin");
        public static readonly ShipmentTypeStatus Cancelled = new ShipmentTypeStatus("Cancelled");
        public static readonly ShipmentTypeStatus OnHold = new ShipmentTypeStatus("On Hold");

        public static readonly IEnumerable<ShipmentTypeStatus> All = new List<ShipmentTypeStatus>
        {
            Pending,
            Processing,
            ReadyForPickup,
            Shipped,
            InTransit,
            OutForDelivery,
            Delivered,
            DeliveryFailed,
            ReturnedToOrigin,
            Cancelled,
            OnHold
        };

        public override string ToString() => Value;

        public static ShipmentTypeStatus FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            var match = All.FirstOrDefault(s =>
                s.Value.Equals(value, StringComparison.OrdinalIgnoreCase));

            if (match == null)
                throw new ArgumentException($"Invalid Shipment Status: {value}");

            return match;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ShipmentTypeStatus;
            return other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}

