using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Controllers;
using System;
using System.Threading.Tasks;

using Morpho.Application.IoT.Services;

namespace Morpho.Web.Host.Controllers.IoT
{
    [Route("tms/iot/devices")]
    public class DeviceSyncController : AbpController
    {
        private readonly DeviceSyncAppService _deviceSyncAppService;

        public DeviceSyncController(DeviceSyncAppService deviceSyncAppService)
        {
            _deviceSyncAppService = deviceSyncAppService;
        }

        // ------------------------------------------------------------
        // /tms/iot/devices/{id}/sync-status
        // ------------------------------------------------------------
        [HttpPost("{id}/sync-status")]
        public async Task<IActionResult> SyncStatus(Guid id)
        {
            var result = await _deviceSyncAppService.SyncStatusAsync(id);
            return Ok(result);
        }

        // ------------------------------------------------------------
        // /tms/iot/devices/{id}/sync-config
        // ------------------------------------------------------------
        [HttpPost("{id}/sync-config")]
        public async Task<IActionResult> SyncConfig(Guid id)
        {
            var result = await _deviceSyncAppService.SyncConfigAsync(id);
            return Ok(result);
        }

        // ------------------------------------------------------------
        // /tms/iot/devices/{id}/sync-logs
        // ------------------------------------------------------------
        [HttpPost("{id}/sync-logs")]
        public async Task<IActionResult> SyncLogs(Guid id)
        {
            var result = await _deviceSyncAppService.SyncLogsAsync(id);
            return Ok(result);
        }

        // ------------------------------------------------------------
        // /tms/iot/devices/{id}/reboot
        // ------------------------------------------------------------
        [HttpPost("{id}/reboot")]
        public async Task<IActionResult> Reboot(Guid id)
        {
            var success = await _deviceSyncAppService.RebootAsync(id);
            return Ok(new { success });
        }

        // ------------------------------------------------------------
        // /tms/iot/devices/{id}/clear-logs
        // ------------------------------------------------------------
        [HttpPost("{id}/clear-logs")]
        public async Task<IActionResult> ClearLogs(Guid id)
        {
            var success = await _deviceSyncAppService.ClearLogsAsync(id);
            return Ok(new { success });
        }
    }
}
