using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Morpho.Domain.Services.Telemetry;
using Morpho.Domain.ValueObjects;
using Morpho.Telemetry.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Morpho.Telemetry
{
    public class TelemetryAppService : ApplicationService
    {
        private readonly TelemetryDomainService _telemetryDomainService;

        public TelemetryAppService(TelemetryDomainService telemetryDomainService)
        {
            _telemetryDomainService = telemetryDomainService;
        }

        public async Task<ListResultDto<Guid>> IngestDeviceStatusAsync(DeviceStatusInputDto input)
        {
            // Convert Unix timestamp → UTC DateTime
            var timestamp = DateTimeOffset
                .FromUnixTimeSeconds(input.timestamp)
                .UtcDateTime;

            // GPS Value Object (all doubles → GpsLocation expects double)
            var gps = new GpsLocation(
                input.latitude,
                input.longitude,
                input.altitude,
                input.accuracy
            );

            // DeviceId is already a GUID → pass directly
            Guid deviceId = input.device_id.Value;

            // Convert all telemetry doubles → decimals
            var battery = (decimal)input.battery_level;
            var temp = (decimal)input.temperature;
            var hum = (decimal)input.humidity;
            var light = (decimal)input.light;
            var vib = (decimal)input.mean_vibration;

            // Save into domain service
            var records = await _telemetryDomainService.RecordMultiSensorAsync(
                input.tenant_id,
                deviceId,
                timestamp,
                gps,
                battery,
                temp,
                hum,
                light,
                vib
            );

            // Return Ids only
            return new ListResultDto<Guid>(
               records.Select(r => r.Id).ToList()
            );
        }
    }
}
