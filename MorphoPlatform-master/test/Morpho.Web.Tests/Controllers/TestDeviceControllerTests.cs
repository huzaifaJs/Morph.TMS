using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Morpho.Web.Tests.Controllers
{
    public class TestDeviceControllerTests : MorphoWebTestBase
    {
        // GET /api/test/devices/ping
        [Fact]
        public async Task Should_Return_Pong()
        {
            var response = await Client.GetAsync("/api/test/devices/ping");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var json = await response.Content.ReadFromJsonAsync<dynamic>();
            ((string)json.message).ShouldBe("pong");
        }

        // GET /api/test/devices/{id}
        [Fact]
        public async Task Should_Get_Device_By_Id()
        {
            var response = await Client.GetAsync("/api/test/devices/10");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var json = await response.Content.ReadFromJsonAsync<dynamic>();
            ((int)json.device_id).ShouldBe(10);
        }

        [Fact]
        public async Task Should_Return_BadRequest_For_Invalid_Id()
        {
            var response = await Client.GetAsync("/api/test/devices/0");

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        // POST /api/test/devices
        [Fact]
        public async Task Should_Create_Device()
        {
            var body = new { name = "Test Device" };

            var response = await Client.PostAsJsonAsync("/api/test/devices", body);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var json = await response.Content.ReadFromJsonAsync<dynamic>();
            ((string)json.status).ShouldBe("created");
            ((string)json.name).ShouldBe("Test Device");
        }
    }
}
