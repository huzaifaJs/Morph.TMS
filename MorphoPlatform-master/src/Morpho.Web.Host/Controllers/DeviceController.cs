using Abp.AspNetCore.Mvc.Controllers;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Morpho.Application.Integration.MorphoApi;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Services;
using Morpho.Domain.Services.Telemetry;
using Morpho.Domain.ValueObjects;
using Morpho.Integration.MorphoApi;
using Morpho.Integration.MorphoApi.Dto;
using System;
using System.Threading.Tasks;

namespace Morpho.Web.Host.Controllers
{
    [ApiController]
    [Route("api/device")]
    public class DeviceController : Morpho.Controllers.MorphoControllerBase //AbpController
    {
        private readonly IRepository<IoTDevice, Guid> _deviceRepository;
        private readonly IMorphoApiClient _morphoApiClient;
        private readonly TelemetryDomainService _telemetryService;
        private readonly IEventService _eventService;
        private readonly DeviceConfigDomainService _configService;
        private readonly DeviceLogDomainService _logService;

        public DeviceController(
            IRepository<IoTDevice, Guid> deviceRepository,
            IMorphoApiClient morphoApiClient,
            TelemetryDomainService telemetryService,
               IEventService eventService,
            DeviceConfigDomainService configService,
            DeviceLogDomainService logService)
        {
            _deviceRepository = deviceRepository;
            _morphoApiClient = morphoApiClient;
            _telemetryService = telemetryService;
            _eventService = eventService;
            _configService = configService;
            _logService = logService;
        }

        // ============================================================
        // Helper
        // ============================================================
        private async Task<IoTDevice> FindDeviceAsync(int deviceId)
        {
            var device = await _deviceRepository.FirstOrDefaultAsync(d => d.MorphoDeviceId == deviceId);
            if (device == null)
                throw new UserFriendlyException($"Device with device_id={deviceId} not found.");
            return device;
        }


        // ============================================================
        // STATUS
        // ============================================================

        // GET /api/device/status
        [HttpGet("status")]
        public async Task<DeviceStatusResponseDto> GetStatus(int device_id)
        {
            var device = await FindDeviceAsync(device_id);

            var status = await _morphoApiClient.GetDeviceStatusAsync(device.MorphoDeviceId);
            await _telemetryService.RecordStatusFromMorphoAsync(device, status);
            return status;
        }

        // POST /api/device/status
        //[HttpPost("status")]
        //public async Task<IActionResult> PostStatus([FromBody] DeviceStatusResponseDto dto)
        //{
        //    var device = await FindDeviceAsync(dto.device_id);
        //    await _telemetryService.RecordStatusFromMorphoAsync(device, dto);

        //    return Ok();
        //}
        [HttpPost("status")]
        public async Task<IActionResult> PostStatus([FromBody] MorphoTelemetryPushDto dto)
        {
            var device = await FindDeviceAsync(dto.device_id);

            await _telemetryService.RecordTelemetryPushAsync(device, dto);

            // also process events (threshold exceeded etc.)
            await _eventService.ProcessEventAsync(device, dto);

            return Ok();
        }


        // ============================================================
        // CONFIG
        // ============================================================

        // GET /api/device/config
        [HttpGet("config")]
        public async Task<DeviceConfigResponseDto> GetConfig(int device_id)
        {
            var device = await FindDeviceAsync(device_id);

            var config = await _morphoApiClient.GetDeviceConfigAsync(device.MorphoDeviceId);
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
        public async Task<DeviceConfigResponseDto> GetConfigResponse(int device_id)
        {
            return await GetConfig(device_id);
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
        public async Task<DeviceLogsResponseDto> GetLogs(int device_id)
        {
            var device = await FindDeviceAsync(device_id);

            var logs = await _morphoApiClient.GetDeviceLogsAsync(device.MorphoDeviceId);
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

        [HttpGet("clear-logs")]
        public async Task<DeviceClearLogsCommandDto> GetClearLogsCommand(int device_id)
        {          

            return await Task.FromResult(new DeviceClearLogsCommandDto
            {
                device_id = device_id,
                clear = true
            });
        }

        //// POST /api/device/clear-logs
        [HttpPost("clear-logs")]
        public async Task<IActionResult> PostClearLogsCommand([FromBody] DeviceClearLogsCommandDto dto)
        {
            var device = await FindDeviceAsync(dto.device_id);

            await _morphoApiClient.PostDeviceClearLogsAsync(
                new DeviceClearLogsPushDto
                {
                    device_id = device.MorphoDeviceId,
                    clear = true
                });

            return Ok();
        }


        //// GET /api/device/clear-logs-response
        [HttpGet("clear-logs-response")]
        public async Task<DeviceClearLogsResponseDto> GetClearLogsResponse(int device_id)
        {
            // This is the expected REPLY that Morpho device sends back
            return new DeviceClearLogsResponseDto
            {
                device_id = device_id,
                cleared = true,
                message = "Logs cleared successfully"
            };
        }


        //// POST /api/device/clear-logs-response
        [HttpPost("clear-logs-response")]
        public async Task<IActionResult> PostClearLogsResponse([FromBody] DeviceClearLogsResponseDto dto)
        {
            // Save response if needed
            return Ok();
        }

        //// ============================================================
        //// REBOOT (client wants it visible)
        //// ============================================================

        //// GET /api/device/reboot
        [HttpGet("reboot")]
        public async Task<DeviceRebootResponseDto> GetRebootCommand(int device_id)
        {
            return new DeviceRebootResponseDto
            {
                device_id = device_id,
                rebooted = true
            };
        }

        //// POST /api/device/reboot
        [HttpPost("reboot")]
        public async Task<IActionResult> PostRebootCommand([FromBody] DeviceRebootPushDto dto)
        {
            if (dto == null || dto.device_id <= 0)
                return BadRequest("Invalid device_id");

            // Forward reboot command to Morpho API
            await _morphoApiClient.PostDeviceRebootAsync(dto);

            return Ok(new
            {
                device_id = dto.device_id,
                reboot = true,
                message = "Reboot command sent successfully"
            });
        }


        [HttpGet("reboot-response")]
        public async Task<DeviceRebootResponseDto> GetRebootResponse(int device_id)
        {
            // Call the Morpho API client
            var result = await _morphoApiClient.GetDeviceRebootResponseAsync(device_id);

            // Always return a DTO — required for Web API
            return result ?? new DeviceRebootResponseDto
            {
                device_id = device_id,
                rebooted = false,
                message = "No response received from Morpho API."
            };
        }

        //// POST /api/device/reboot-response
        [HttpPost("reboot-response")]
        public async Task<IActionResult> PostRebootResponse([FromBody] DeviceRebootResponseDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid payload.");

            // Forward to Morpho API (if required)
            await _morphoApiClient.PostDeviceRebootResponseAsync(dto);

            // Return acknowledgment back to caller
            var response = new DeviceRebootResponseDto
            {
                device_id = dto.device_id,
                rebooted = dto.rebooted,
                message = dto.message ?? "Device reboot acknowledged"
            };

            return Ok(response);
        }
        [HttpPost("telemetry")]
        public async Task<IActionResult> PostTelemetry([FromBody] MorphoTelemetryPushDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid telemetry payload.");

            // 1. Find IoTDevice using morpho_device_id
            var device = await _deviceRepository.FirstOrDefaultAsync(d => d.MorphoDeviceId == dto.device_id);
            if (device == null)
                throw new UserFriendlyException($"Device with device_id={dto.device_id} not found.");

            // 2. GPS object
            var gps = dto.gps != null
                ? new GpsLocation(dto.gps.latitude, dto.gps.longitude, dto.gps.altitude, dto.gps.accuracy)
                : null;

            // 3. Call CORRECT domain method
            await _telemetryService.RecordMorphoTelemetryAsync(
                device,
                timestampRaw: dto.timestamp,
                gps: gps,
                rssi: dto.rssi,
                battery: dto.battery_level,
                temperature: dto.temperature,
                humidity: dto.humidity,
                light: dto.light,
                vibration: dto.mean_vibration,
                status: dto.status,
                nbrfid: dto.nbrfid
            );

            return Ok();
        }


        [HttpPost("event")]
        public async Task<IActionResult> PostEvent([FromBody] MorphoEventPushDto dto)
        {
            await _eventService.RecordMorphoEventAsync(dto);
            return Ok();
        }


    }
}
