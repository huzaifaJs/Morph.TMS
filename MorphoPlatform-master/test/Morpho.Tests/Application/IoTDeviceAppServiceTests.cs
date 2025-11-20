using Morpho.Application.IoT;
using Morpho.IoT.Dto;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Morpho.Tests.Application
{
    public class IoTDeviceAppServiceTests : MorphoTestBase
    {
        private readonly IIoTDeviceAppService _service;

        public IoTDeviceAppServiceTests()
        {
            _service = Resolve<IIoTDeviceAppService>();
        }

        [Fact]
        public async Task Should_Register_And_Get_Device()
        {
            var dto = new CreateIoTDeviceDto
            {
                tenant_id = 1,
                external_device_id = "EXT1000",
                serial_number = "SN1000",
                name = "TestDevice",
                device_type = "sensor"
            };

            var result = await _service.RegisterAsync(dto);
            result.Id.ShouldNotBe(Guid.Empty);

            var get = await _service.GetAsync(result.Id);
            get.name.ShouldBe("TestDevice");
        }
    }
}
