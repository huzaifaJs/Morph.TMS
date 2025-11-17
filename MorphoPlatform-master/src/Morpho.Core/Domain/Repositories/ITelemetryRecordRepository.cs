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
        Task<List<TelemetryRecord>> GetLatestForDeviceAsync(
            int tenantId,
            Guid deviceId,
            int maxCount = 100);

        Task<TelemetryRecord> GetLastForSensorAsync(
            int tenantId,
            Guid deviceId,
            SensorType sensorType);

        Task<List<TelemetryRecord>> GetForShipmentAsync(
            int tenantId,
            Guid shipmentId);
    }
}
