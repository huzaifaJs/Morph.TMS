using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Morpho.Web.Tests.Controllers
{
    [Route("api/test/devices")]
    public class TestDeviceController : ControllerBase
    {
        // GET /api/test/devices/ping
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "pong" });
        }

        // GET /api/test/devices/{id}
        [HttpGet("{id}")]
        public IActionResult GetDevice(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            return Ok(new { device_id = id, name = "Demo Device" });
        }

        // POST /api/test/devices
        [HttpPost]
        public IActionResult CreateDevice([FromBody] DeviceCreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.name))
                return BadRequest("Name required");

            return Ok(new
            {
                device_id = 999,
                name = request.name,
                status = "created"
            });
        }
    }

    public class DeviceCreateRequest
    {
        public string name { get; set; }
    }
}
