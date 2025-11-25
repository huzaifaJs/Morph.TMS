using Abp.Domain.Services;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Entities.Telemetry;
using Morpho.Domain.Enums;
using Morpho.Domain.Repositories;
using Morpho.Domain.ValueObjects;
using Morpho.Integration.MorphoApi;
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
        public async Task<TelemetryRecord> RecordMultiSensorAsync(
         IoTDevice device,
         long timestamp,
         GpsLocation gps,
         double rssi,
         double battery,
         double temperature,
         double humidity,
         double light,
         double vibration,
         string status,
         int nbrfid)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            // Create telemetry row exactly as client sends
            var record = new TelemetryRecord(
                tenantId: device.TenantId,
                deviceId: device.Id,
                timestamp: timestamp,
                firmwareVersion: device.FirmwareVersion, // if you store it; optional
                ip: device.IpAddress,                 // optional
                gps: gps,
                rssi: rssi,
                battery: battery,
                temp: temperature,
                humidity: humidity,
                vibration: vibration,
                light: light,
                status: status,
                nbrfid: nbrfid
            );

            await _telemetryRepository.InsertAsync(record);

            return record;
        }

        public async Task RecordTelemetryPushAsync(IoTDevice device, MorphoTelemetryPushDto dto)
        {
            if (dto == null)
                return;

            // 1. Update status
            var statusEnum = dto.status.ToDeviceStatusType();
            device.UpdateStatus(statusEnum);

            // 2. Update last known location
            if (dto.gps != null)
            {
                device.SetLastKnownLocation(dto.gps.latitude, dto.gps.longitude);
            }

            // 3. Build TelemetryRecord
            var record = new TelemetryRecord(
                tenantId: device.TenantId,
                deviceId: device.Id,
                timestamp: dto.timestamp,
                firmwareVersion: dto.firmware_version,
                ip: dto.ip_address,
                gps: dto.gps != null ? new GpsLocation(dto.gps.latitude, dto.gps.longitude) : null,
                rssi: dto.rssi,
                battery: dto.battery_level,
                temp: dto.temperature,
                humidity: dto.humidity,
                vibration: dto.mean_vibration,
                light: dto.light,
                status: dto.status,
                nbrfid: dto.nbrfid
            );

            await _telemetryRepository.InsertAsync(record);

            // Save updated device
            await _deviceRepository.UpdateAsync(device);
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

            // Convert timestamp
            DateTime timestampUtc = DateTime.UtcNow;
            if (dto.timestamp > 0)
            {
                try
                {
                    timestampUtc = DateTimeOffset.FromUnixTimeSeconds(dto.timestamp).UtcDateTime;
                }
                catch { }
            }

            // GPS
            var gps = dto.gps != null
                ? new GpsLocation(dto.gps.latitude, dto.gps.longitude)
                : null;

            // 1. UPDATE LAST KNOWN LOCATION
            if (dto.gps != null)
            {
                device.SetLastKnownLocation(dto.gps.latitude, dto.gps.longitude);
                await _deviceRepository.UpdateAsync(device);
            }

            // 2. UPDATE DEVICE STATUS
            device.UpdateStatus(dto.status.ToDeviceStatusType());
            await _deviceRepository.UpdateAsync(device);

            // 3. Create telemetry record (FULL packet)
            var record = new TelemetryRecord(
                tenantId: device.TenantId,
                deviceId: device.Id,
                timestamp: dto.timestamp,
                firmwareVersion: dto.firmware_version,
                ip: dto.ip_address,
                gps: gps,
                rssi: dto.rssi,
                battery: dto.batterie_level,
                temp: dto.temperature,
                humidity: dto.humidity,
                vibration: dto.mean_vibration,
                light: dto.light,
                status: dto.status,
                nbrfid: 0 // if missing, default 0
            );

            await _telemetryRepository.InsertAsync(record);
        }
                public async Task<TelemetryRecord> RecordMorphoTelemetryAsync(
            IoTDevice device,
            long timestampRaw,
            GpsLocation gps,
            double rssi,
            double battery,
            double temperature,
            double humidity,
            double light,
            double vibration,
            string status,
            int nbrfid)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            var record = new TelemetryRecord(
                tenantId: device.TenantId,
                deviceId: device.Id,
                timestamp: timestampRaw,
                firmwareVersion: device.FirmwareVersion, // if populated
                ip: device.IpAddress,                    // if stored
                gps: gps,
                rssi: rssi,
                battery: battery,
                temp: temperature,
                humidity: humidity,
                vibration: vibration,
                light: light,
                status: status,
                nbrfid: nbrfid
            );

            await _telemetryRepository.InsertAsync(record);
            return record;
        }



    }
}
