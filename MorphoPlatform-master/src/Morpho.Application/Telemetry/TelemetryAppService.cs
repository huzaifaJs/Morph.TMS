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
        /// Ingest unified status+telemetry push from Morpho device
        /// </summary>
        public async Task<ListResultDto<Guid>> IngestDeviceStatusAsync(DeviceStatusInputDto input)
        {
            // 1. Resolve IoTDevice using *morpho_device_id*
            var device = await _deviceRepository.FirstOrDefaultAsync(
                d => d.MorphoDeviceId == input.morpho_device_id)
                ?? throw new UserFriendlyException(
                    $"Device with device_id={input.morpho_device_id} not found.");

            // 2. GPS Object
            var gps = new GpsLocation(
                input.latitude,
                input.longitude,
                input.altitude,
                input.accuracy
            );

            // 3. Save Telemetry into Domain Service
            var record = await _telemetryDomainService.RecordMorphoTelemetryAsync(
                device,
                timestampRaw: input.timestamp,
                gps: gps,
                rssi: input.rssi,
                battery: input.battery_level,
                temperature: input.temperature,
                humidity: input.humidity,
                light: input.light,
                vibration: input.mean_vibration,
                status: input.status,
                nbrfid: input.nbrfid
            );

            // 4. Return the created record ID list
            return new ListResultDto<Guid>(
                new List<Guid> { record.Id }
            );
        }
    }
}
