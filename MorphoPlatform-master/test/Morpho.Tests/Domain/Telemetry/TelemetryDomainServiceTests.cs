using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Repositories;
using Morpho.Domain.Services.Telemetry;
using Morpho.Integration.MorphoApi.Dto;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Morpho.Tests.Domain.Telemetry
{
    public class TelemetryDomainServiceTests : MorphoTestBase
    {
        [Fact]
        public async Task Should_Record_Status_And_Update_Location()
        {
            var service = Resolve<TelemetryDomainService>();
            var repo = Resolve<IIoTDeviceRepository>();

            var device = new IoTDevice(1, 7777, "SN1", "Device1", "sensor");
            await repo.InsertAsync(device);

            var dto = new DeviceStatusResponseDto
            {
                gps = new GpsDto { latitude = 10.5, longitude = 20.6 },
                batterie_level = 88.9,
                humidity = 60,
                temperature = 23.2,
                mean_vibration = 1.2,
                light = 200,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };

            await service.RecordStatusFromMorphoAsync(device, dto);

            device.LastKnownLocation.Latitude.ShouldBe(10.5);
            device.LastKnownLocation.Longitude.ShouldBe(20.6);
        }
    }
}
