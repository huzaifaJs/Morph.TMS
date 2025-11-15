using Morpho.Domain.Entities.GeoFencing;
using Morpho.Domain.Interfaces;
using Morpho.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Services
{
    public class GeoFenceService : IGeoFenceService
    {
        public Task<GeoFenceEvent?> EvaluateLocationAsync(
            GeoFenceZone zone,
            GpsLocation location,
            int tenantId,
            Guid deviceId,
            Guid? shipmentId)
        {
            // Skeleton: just returns null for now.
            // Later you implement point-in-polygon / distance checks.
            return Task.FromResult<GeoFenceEvent?>(null);
        }
    }
}
