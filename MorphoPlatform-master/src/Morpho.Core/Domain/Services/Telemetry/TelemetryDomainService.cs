using Abp.Domain.Services;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Enums;
using Morpho.Domain.Repositories;
using Morpho.Domain.ValueObjects;
using Morpho.Integration.MorphoApi.Dto;
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

        // -----------------------------------------------
        // MULTI SENSOR INGESTION (Generic)
        // -----------------------------------------------
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
            var device = await _deviceRepository.FirstOrDefaultAsync(deviceId);
            if (device == null || device.TenantId != tenantId)
                throw new ArgumentException("Device not found for tenant.");

            var records = new List<TelemetryRecord>();

            void AddIfHasValue(SensorType type, decimal? value, string unit)
            {
                if (!value.HasValue) return;

                records.Add(new TelemetryRecord(
                    tenantId,
                    deviceId,
                    type,
                    value.Value,
                    unit,
                    timestamp,
                    gps,
                    batteryLevel
                ));
            }

            AddIfHasValue(SensorType.Temperature, temperature, "C");
            AddIfHasValue(SensorType.Humidity, humidity, "%");
            AddIfHasValue(SensorType.Light, light, "lx");
            AddIfHasValue(SensorType.Vibration, vibration, "g");

            foreach (var r in records)
                await _telemetryRepository.InsertAsync(r);

            return records;
        }

        // -----------------------------------------------
        // MORPHO DEVICE STATUS INGESTION
        // -----------------------------------------------
        public async Task RecordStatusFromMorphoAsync(IoTDevice device, DeviceStatusResponseDto dto)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Timestamp
            DateTime timestampUtc = DateTime.UtcNow;
            if (dto.timestamp > 0)
            {
                try
                {
                    timestampUtc = DateTimeOffset.FromUnixTimeSeconds(dto.timestamp).UtcDateTime;
                }
                catch { }
            }

            // GPS VO
            var gps = dto.gps != null
                ? new GpsLocation(dto.gps.latitude, dto.gps.longitude)
                : null;

            // ------------------------------------------------
            // UPDATE LAST KNOWN LOCATION
            // ------------------------------------------------
            if (dto.gps != null)
            {
                device.SetLastKnownLocation(dto.gps.latitude, dto.gps.longitude);
                await _deviceRepository.UpdateAsync(device);
            }

            // Build telemetry records
            var records = new List<TelemetryRecord>();

            void Add(SensorType type, double? value, string unit)
            {
                if (value == null) return;

                records.Add(new TelemetryRecord(
                    tenantId: device.TenantId,
                    deviceId: device.Id,
                    sensorType: type,
                    value: (decimal)value,
                    unit: unit,
                    timestamp: timestampUtc,
                    gps: gps,
                    batteryLevel: (decimal?)dto.batterie_level
                ));
            }

            Add(SensorType.BatteryLevel, dto.batterie_level, "%");
            Add(SensorType.Temperature, dto.temperature, "C");
            Add(SensorType.Humidity, dto.humidity, "%");
            Add(SensorType.Vibration, dto.mean_vibration, "g");
            Add(SensorType.Light, dto.light, "lx");

            foreach (var item in records)
                await _telemetryRepository.InsertAsync(item);
        }


    }
}
