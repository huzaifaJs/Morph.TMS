using Morpho.Domain.Entities.GeoFencing;
using Morpho.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Interfaces
{
    public interface IGeoFenceService
    {
        Task<GeoFenceEvent?> EvaluateLocationAsync(GeoFenceZone zone, GpsLocation location, int tenantId, Guid deviceId, Guid? shipmentId);
    }
}
