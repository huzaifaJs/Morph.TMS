using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Services.Telemetry;
using Morpho.Domain.ValueObjects;
using Morpho.Integration.MorphoApi.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpho.Telemetry
{
    public class TelemetryAppService : ApplicationService
    {
        private readonly IRepository<IoTDevice, Guid> _deviceRepository;
        private readonly TelemetryDomainService _telemetryDomainService;

        public TelemetryAppService(
            IRepository<IoTDevice, Guid> deviceRepository,
            TelemetryDomainService telemetryDomainService)
        {
            _deviceRepository = deviceRepository;
            _telemetryDomainService = telemetryDomainService;
        }

        /// <summary>
        /// Unified Telemetry ingestion (Morpho device POST → TelemetryRecord)
        /// </summary>
        public async Task<ListResultDto<Guid>> IngestMorphoTelemetryAsync(MorphoTelemetryPushDto dto)
        {
            if (dto == null)
                throw new UserFriendlyException("Telemetry payload cannot be empty.");

            // 1. Resolve IoTDevice using morpho_device_id (INT)
            var device = await _deviceRepository.FirstOrDefaultAsync(
                d => d.MorphoDeviceId == dto.device_id);

            if (device == null)
                throw new UserFriendlyException($"Device with device_id={dto.device_id} not found.");


            // 2. Build GPS Value Object
            var gps = dto.gps != null
                ? new GpsLocation(
                    dto.gps.latitude,
                    dto.gps.longitude,
                    dto.gps.altitude,
                    dto.gps.accuracy
                )
                : null;

            // 3. Pass to DomainService (records TelemetryRecord)
            var record = await _telemetryDomainService.RecordMorphoTelemetryAsync(
                device,
                timestampRaw: dto.timestamp,
                gps: gps,
                rssi: dto.rssi,
                battery: dto.battery_level,
                temperature: dto.temperature,
                humidity: dto.humidity,
                light: dto.light,
                vibration: dto.mean_vibration,
                status: dto.status,
                nbrfid: dto.nbrfid
            );

            // 4. Return created TelemetryRecord ID
            return new ListResultDto<Guid>(
                new List<Guid> { record.Id }
            );
        }
    }
}
