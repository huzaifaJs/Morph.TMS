using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Morpho.Application.IoT;
using Morpho.IoT.Dto;

namespace Morpho.Web.Host.Controllers
{
    [Route("tms/iot/devices")]
    [ApiController]   // IMPORTANT in ASP.NET Core
    public class TmsIotDevicesController : ControllerBase
    {
        private readonly IIoTDeviceAppService _deviceAppService;

        public TmsIotDevicesController(IIoTDeviceAppService deviceAppService)
        {
            _deviceAppService = deviceAppService;
        }

        // POST tms/iot/devices/register
        [HttpPost("register")]
        public async Task<IoTDeviceDto> Register([FromBody] CreateIoTDeviceDto input)
        {
            return await _deviceAppService.RegisterAsync(input);
        }

        // GET tms/iot/devices/{id}
        [HttpGet("{id:guid}")]
        public async Task<IoTDeviceDto> Get(Guid id)
        {
            return await _deviceAppService.GetAsync(id);
        }

        // GET tms/iot/devices?Keyword=abc&SkipCount=0&MaxResultCount=10
        [HttpGet]
        public async Task<PagedResultDto<IoTDeviceDto>> GetList([FromQuery] GetIoTDevicesInputDto input)
        {
            return await _deviceAppService.GetListAsync(input);
        }
    }
}
