using Abp.Events.Bus;
using Morpho.Domain.Entities.GeoFencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Events
{
    public class GeoFenceEventOccurred : EventData
    {
        public GeoFenceEvent Event { get; }

        public GeoFenceEventOccurred(GeoFenceEvent @event)
        {
            Event = @event;
        }
    }
}
