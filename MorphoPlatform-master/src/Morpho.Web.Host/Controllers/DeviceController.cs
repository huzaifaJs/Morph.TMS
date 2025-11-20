using Abp.AspNetCore.Mvc.Controllers;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Morpho.Application.Integration.MorphoApi;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Services;
using Morpho.Domain.Services.Telemetry;
using Morpho.Integration.MorphoApi;
using Morpho.Integration.MorphoApi.Dto;
using System;
using System.Threading.Tasks;

namespace Morpho.Web.Host.Controllers
{
    [ApiController]
    [Route("api/device")]
    public class DeviceController : AbpController
    {
        private readonly IRepository<IoTDevice, Guid> _deviceRepository;
        private readonly IMorphoApiClient _morphoApiClient;
        private readonly TelemetryDomainService _telemetryService;
        private readonly DeviceConfigDomainService _configService;
        private readonly DeviceLogDomainService _logService;

        public DeviceController(
            IRepository<IoTDevice, Guid> deviceRepository,
            IMorphoApiClient morphoApiClient,
            TelemetryDomainService telemetryService,
            DeviceConfigDomainService configService,
            DeviceLogDomainService logService)
        {
            _deviceRepository = deviceRepository;
            _morphoApiClient = morphoApiClient;
            _telemetryService = telemetryService;
            _configService = configService;
            _logService = logService;
        }

        // ============================================================
        // Helper
        // ============================================================
        private async Task<IoTDevice> FindDeviceAsync(int deviceId)
        {
            var externalId = deviceId.ToString();

            var device = await _deviceRepository.FirstOrDefaultAsync(d => d.ExternalDeviceId == externalId);
            if (device == null)
                throw new UserFriendlyException($"Device with device_id={deviceId} not found.");

            return device;
        }

        // ============================================================
        // STATUS
        // ============================================================

        // GET /api/device/status
        [HttpGet("status")]
        public async Task<DeviceStatusResponseDto> GetStatus([FromQuery(Name = "device_id")] int deviceId)
        {
            var device = await FindDeviceAsync(deviceId);

            var status = await _morphoApiClient.GetDeviceStatusAsync(device.ExternalDeviceId);
            await _telemetryService.RecordStatusFromMorphoAsync(device, status);

            return status;
        }

        // POST /api/device/status
        [HttpPost("status")]
        public async Task<IActionResult> PostStatus([FromBody] DeviceStatusResponseDto dto)
        {
            var device = await FindDeviceAsync(dto.device_id);
            await _telemetryService.RecordStatusFromMorphoAsync(device, dto);

            return Ok();
        }

        // ============================================================
        // CONFIG
        // ============================================================

        // GET /api/device/config
        [HttpGet("config")]
        public async Task<DeviceConfigResponseDto> GetConfig([FromQuery(Name = "device_id")] int deviceId)
        {
            var device = await FindDeviceAsync(deviceId);

            var config = await _morphoApiClient.GetDeviceConfigAsync(device.ExternalDeviceId);
            await _configService.UpdateFromMorphoAsync(device, config);

            return config;
        }

        // POST /api/device/config
        [HttpPost("config")]
        public async Task<IActionResult> PostConfig([FromBody] DeviceConfigResponseDto dto)
        {
            var device = await FindDeviceAsync(dto.device_id);
            await _configService.UpdateFromMorphoAsync(device, dto);

            return Ok();
        }

        // GET /api/device/config-response
        [HttpGet("config-response")]
        public async Task<DeviceConfigResponseDto> GetConfigResponse([FromQuery(Name = "device_id")] int deviceId)
        {
            return await GetConfig(deviceId);
        }

        // POST /api/device/config-response
        [HttpPost("config-response")]
        public async Task<IActionResult> PostConfigResponse([FromBody] DeviceConfigResponseDto dto)
        {
            return await PostConfig(dto);
        }

        // ============================================================
        // LOGS
        // ============================================================

        // GET /api/device/logs
        [HttpGet("logs")]
        public async Task<DeviceLogsResponseDto> GetLogs([FromQuery(Name = "device_id")] int deviceId)
        {
            var device = await FindDeviceAsync(deviceId);

            var logs = await _morphoApiClient.GetDeviceLogsAsync(device.ExternalDeviceId);
            await _logService.AppendFromMorphoAsync(device, logs);

            return logs;
        }

        // POST /api/device/logs
        [HttpPost("logs")]
        public async Task<IActionResult> PostLogs([FromBody] DeviceLogsResponseDto dto)
        {
            var device = await FindDeviceAsync(dto.device_id);
            await _logService.AppendFromMorphoAsync(device, dto);

            return Ok();
        }

        // ============================================================
        // CLEAR LOGS (TODO:This is a dummy API and we have to implement this)
        // ============================================================

        //// GET /api/device/clear-logs
        //[HttpGet("clear-logs")]
        //public async Task<ClearLogsCommandDto> GetClearLogsCommand([FromQuery(Name = "device_id")] int deviceId)
        //{
        //    // “Get Clear Logs Command”
        //    return new ClearLogsCommandDto
        //    {
        //        device_id = deviceId,
        //        clear = true
        //    };
        //}

        //// POST /api/device/clear-logs
        //[HttpPost("clear-logs")]
        //public async Task<IActionResult> PostClearLogsCommand([FromBody] ClearLogsCommandDto dto)
        //{
        //    var device = await FindDeviceAsync(dto.device_id);

        //    await _morphoApiClient.SendClearLogsCommandAsync(device.ExternalDeviceId);

        //    return Ok();
        //}

        //// GET /api/device/clear-logs-response
        //[HttpGet("clear-logs-response")]
        //public async Task<ClearLogsResponseDto> GetClearLogsResponse([FromQuery(Name = "device_id")] int deviceId)
        //{
        //    return new ClearLogsResponseDto
        //    {
        //        device_id = deviceId,
        //        status = "received"
        //    };
        //}

        //// POST /api/device/clear-logs-response
        //[HttpPost("clear-logs-response")]
        //public async Task<IActionResult> PostClearLogsResponse([FromBody] ClearLogsResponseDto dto)
        //{
        //    // Save response if needed
        //    return Ok();
        //}

        //// ============================================================
        //// REBOOT (client wants it visible)
        //// ============================================================

        //// GET /api/device/reboot
        //[HttpGet("reboot")]
        //public async Task<RebootCommandDto> GetRebootCommand([FromQuery(Name = "device_id")] int deviceId)
        //{
        //    return new RebootCommandDto
        //    {
        //        device_id = deviceId,
        //        reboot = true
        //    };
        //}

        //// POST /api/device/reboot
        //[HttpPost("reboot")]
        //public async Task<IActionResult> PostRebootCommand([FromBody] RebootCommandDto dto)
        //{
        //    var device = await FindDeviceAsync(dto.device_id);

        //    await _morphoApiClient.SendRebootCommandAsync(device.ExternalDeviceId);

        //    return Ok();
        //}

        //// GET /api/device/reboot-response
        //[HttpGet("reboot-response")]
        //public async Task<RebootResponseDto> GetRebootResponse([FromQuery(Name = "device_id")] int deviceId)
        //{
        //    return new RebootResponseDto
        //    {
        //        device_id = deviceId,
        //        status = "received"
        //    };
        //}

        //// POST /api/device/reboot-response
        //[HttpPost("reboot-response")]
        //public async Task<IActionResult> PostRebootResponse([FromBody] RebootResponseDto dto)
        //{
        //    return Ok();
        //}
    }
}
