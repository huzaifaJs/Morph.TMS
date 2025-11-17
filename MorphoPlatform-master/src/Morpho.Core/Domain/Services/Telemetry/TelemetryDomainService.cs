using Abp.Domain.Services;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Enums;
using Morpho.Domain.Repositories;
using Morpho.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpho.Domain.Services.Telemetry
{
    public class TelemetryDomainService : DomainService
    {
        private readonly ITelemetryRecordRepository _telemetryRepository;
        private readonly IIoTDeviceRepository _deviceRepository;

        public TelemetryDomainService(
            ITelemetryRecordRepository telemetryRepository,
            IIoTDeviceRepository deviceRepository)
        {
            _telemetryRepository = telemetryRepository;
            _deviceRepository = deviceRepository;
        }

        public async Task<IReadOnlyList<TelemetryRecord>> RecordMultiSensorAsync(
            int tenantId,
            Guid deviceId,
            DateTime timestamp,
            GpsLocation gps,
            decimal? batteryLevel,
            decimal? temperature,
            decimal? humidity,
            decimal? light,
            decimal? vibration)
        {
            // validate device exists & belongs to tenant
            var device = await _deviceRepository.FirstOrDefaultAsync(deviceId);
            if (device == null || device.TenantId != tenantId)
            {
                throw new ArgumentException("Device not found for tenant.");
            }

            var records = new List<TelemetryRecord>();

            void AddIfHasValue(SensorType type, decimal? value, string unit)
            {
                if (!value.HasValue) return;

                var record = new TelemetryRecord(
                    tenantId,
                    deviceId,
                    type,
                    value.Value,
                    unit,
                    timestamp,
                    gps,
                    batteryLevel);

                records.Add(record);
            }

            AddIfHasValue(SensorType.Temperature, temperature, "C");
            AddIfHasValue(SensorType.Humidity, humidity, "%");
            AddIfHasValue(SensorType.Light, light, "lx");
            AddIfHasValue(SensorType.Vibration, vibration, "g");

            foreach (var r in records)
            {
                await _telemetryRepository.InsertAsync(r);
            }

            return records;
        }
    }
}
