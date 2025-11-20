using Morpho.IoT.Dto;
using Morpho.Web.Host.Controllers;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Morpho.Web.Tests.Controllers
{
    public class TmsIotDevicesControllerTests : MorphoWebTestBase
    {
        private readonly TmsIotDevicesController _controller;

        public TmsIotDevicesControllerTests()
        {
            _controller = Resolve<TmsIotDevicesController>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Devices()
        {
            var result = await _controller.GetList(new GetIoTDevicesInputDto());

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
        }
    }
}
