using Abp.Domain.Repositories;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpho.Domain.Repositories
{
    public interface ITelemetryRecordRepository : IRepository<TelemetryRecord, Guid>
    {
        Task<double?> GetLatestHumidityAsync(int tenantId, Guid deviceId);
        Task<double?> GetLatestTemperatureAsync(int tenantId, Guid deviceId);

       
            Task<List<TelemetryRecord>> GetLatestForDeviceAsync(
            int tenantId,
            Guid deviceId,
            int maxCount = 100);

        Task<List<TelemetryRecord>> GetForShipmentAsync(
            int tenantId,
            Guid shipmentId);
        Task<TelemetryRecord?> GetLatestTelemetryAsync(int tenantId, Guid deviceId);

    }
}
