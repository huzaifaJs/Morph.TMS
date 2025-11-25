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
        public async Task<double?> GetLatestHumidityAsync(int tenantId, Guid deviceId)
        {
            var t = await GetAll()
                .Where(x => x.TenantId == tenantId && x.DeviceId == deviceId)
                .OrderByDescending(x => x.TimestampUtc)
                .FirstOrDefaultAsync();

            return t?.Humidity;
        }

        public async Task<double?> GetLatestTemperatureAsync(int tenantId, Guid deviceId)
        {
            var t = await GetAll()
                .Where(x => x.TenantId == tenantId && x.DeviceId == deviceId)
                .OrderByDescending(x => x.TimestampUtc)
                .FirstOrDefaultAsync();

            return t?.Temperature;
        }

        public async Task<List<TelemetryRecord>> GetLatestForDeviceAsync(
            int tenantId,
            Guid deviceId,
            int maxCount = 100)
        {
            return await GetAll()
                .Where(x => x.TenantId == tenantId && x.DeviceId == deviceId)
                .OrderByDescending(x => x.TimestampRaw)
                .Take(maxCount)
                .ToListAsync();
        }

        public async Task<TelemetryRecord?> GetLatestTelemetryAsync(int tenantId, Guid deviceId)
        {
            return await GetAll()
                .Where(x => x.TenantId == tenantId && x.DeviceId == deviceId)
                .OrderByDescending(x => x.TimestampUtc)
                .FirstOrDefaultAsync();
        }


        public async Task<List<TelemetryRecord>> GetForShipmentAsync(
            int tenantId,
            Guid shipmentId)
        {
            return await GetAll()
                .Where(x => x.TenantId == tenantId && x.ShipmentId == shipmentId)
                .OrderBy(x => x.TimestampRaw)
                .ToListAsync();
        }
    }
}
