using Morpho.Application.IoT.Services;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Enums;
using Morpho.Domain.Repositories;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using Abp.Domain.Repositories;


namespace Morpho.Tests.Application.Integration
{
    public class SyncStatusTests : MorphoTestBase
    {
        private readonly IDeviceSyncAppService _syncService;
        private readonly IIoTDeviceRepository _deviceRepo;

        public SyncStatusTests()
        {
            _syncService = Resolve<IDeviceSyncAppService>();
            _deviceRepo = Resolve<IIoTDeviceRepository>();
        }

        [Fact]
        public async Task SyncStatus_Should_Update_Location_And_Status()
        {
            var device = new IoTDevice(1, "EXT200", "SN200", "D200", "sensor");
            await _deviceRepo.InsertAsync(device);

            var result = await _syncService.SyncStatusAsync(device.Id);

            result.ShouldNotBeNull();

            var updated = await _deviceRepo.GetAsync(device.Id);
            updated.LastKnownLocation.ShouldNotBeNull();
            updated.Status.ShouldNotBe(DeviceStatusType.Unknown);
        }
    }
}
