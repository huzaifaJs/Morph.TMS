using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Enums
{
    public enum ShipmentStatus
    {
        Draft = 0,
        Created = 1,
        InTransit = 2,
        Delivered = 3,
        Cancelled = 4,
        Closed = 5
    }
}
