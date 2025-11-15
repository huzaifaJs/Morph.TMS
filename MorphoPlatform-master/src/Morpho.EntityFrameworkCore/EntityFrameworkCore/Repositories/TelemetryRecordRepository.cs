using Abp.EntityFrameworkCore;
using Abp.Dependency;
using Microsoft.EntityFrameworkCore;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Enums;
using Morpho.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Morpho.EntityFrameworkCore.Repositories;

namespace Morpho.EntityFrameworkCore.EntityFrameworkCore.Repositories
{
    public class TelemetryRecordRepository :
        MorphoRepositoryBase<TelemetryRecord, Guid>,
        ITelemetryRecordRepository,
        ITransientDependency // auto-registered in IoC
    {
        public TelemetryRecordRepository(
            IDbContextProvider<MorphoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<TelemetryRecord>> GetLatestForDeviceAsync(
            int tenantId,
            Guid deviceId,
            int maxCount = 100)
        {
            return await GetAll()
                .Where(x => x.TenantId == tenantId && x.DeviceId == deviceId)
                .OrderByDescending(x => x.Timestamp)
                .Take(maxCount)
                .ToListAsync();
        }

        public async Task<TelemetryRecord> GetLastForSensorAsync(
            int tenantId,
            Guid deviceId,
            SensorType sensorType)
        {
            return await GetAll()
                .Where(x => x.TenantId == tenantId
                         && x.DeviceId == deviceId
                         && x.SensorType == sensorType)
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TelemetryRecord>> GetForShipmentAsync(
            int tenantId,
            Guid shipmentId)
        {
            return await GetAll()
                .Where(x => x.TenantId == tenantId && x.ShipmentId == shipmentId)
                .OrderBy(x => x.Timestamp)
                .ToListAsync();
        }
    }
}
