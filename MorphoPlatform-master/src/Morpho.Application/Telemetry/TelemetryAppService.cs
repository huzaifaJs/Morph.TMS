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
                .FromUnixTimeSeconds(input.Timestamp)
                .UtcDateTime;

            // GPS Value Object (all doubles → GpsLocation expects double)
            var gps = new GpsLocation(
                input.Latitude,
                input.Longitude,
                input.Altitude,
                input.Accuracy
            );

            // DeviceId is already a GUID → pass directly
            Guid deviceId = input.DeviceId;

            // Convert all telemetry doubles → decimals
            var battery = (decimal)input.BatteryLevel;
            var temp = (decimal)input.Temperature;
            var hum = (decimal)input.Humidity;
            var light = (decimal)input.Light;
            var vib = (decimal)input.MeanVibration;

            // Save into domain service
            var records = await _telemetryDomainService.RecordMultiSensorAsync(
                input.TenantId,
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
