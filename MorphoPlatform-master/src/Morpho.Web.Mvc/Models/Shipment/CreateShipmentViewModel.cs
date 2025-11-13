using System.Collections.Generic;

namespace Morpho.Web.Models.Shipment
{
    public class CreateShipmentViewModel
    {
        public string ShipmentReference { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }        
        public List<string> AvailableDevices { get; set; } = new();
    }
}
