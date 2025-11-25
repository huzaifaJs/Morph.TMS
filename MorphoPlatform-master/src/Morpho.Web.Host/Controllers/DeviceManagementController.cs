using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Morpho.Controllers;
using Morpho.Device.TrackingDevice;
using Morpho.Device.TrackingDeviceDto;
using Morpho.Dto;
using Morpho.FuelType;
using Morpho.VehicleDocsType;
using Morpho.VehicleType;
using System;
using System.Threading.Tasks;

namespace Morpho.Web.Host.Controllers
{
    [Route("api/DeviceManagement/")]
    [ApiController]
    public class DeviceManagementController : MorphoControllerBase
    {
        private readonly IDeviceManagementAppService _deviceManagementAppService;

        public DeviceManagementController(IDeviceManagementAppService deviceManagementAppService)
        {
            _deviceManagementAppService = deviceManagementAppService;
        }
        #region  ================================ Device Management ================================
        [HttpGet("GetDeviceListAll")]
        public async Task<IActionResult> GetDeviceListAll()
        {
            try
            {
                var result = await _deviceManagementAppService.GetDeviceListAsync();
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetDeviceDetails")]
        public async Task<IActionResult> GetDeviceDetails(long id)
        {
            try
            {
                var result = await _deviceManagementAppService.GetDeviceDetailsAsync(id);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error fetching details", error = ex.Message });
            }
        }

        [HttpPost("CreateIOTDeviceRegister")]
        public async Task<IActionResult> CreateIOTDeviceRegister(CreateDeviceDto input)
        {
            try
            {
                var result = await _deviceManagementAppService.AddDeviceAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to create IOT device", error = ex.Message });
            }
        }

        [HttpPost("UpdateIOTDevice")]
        public async Task<IActionResult> UpdateIOTDevice([FromBody]UpdateDeviceDto input)
        {
            try
            {
                var result = await _deviceManagementAppService.UpdateDeviceAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to update IOT device", error = ex.Message });
            }
        }

        [HttpPost("UpdateIOTDeviceStatus")]
        public async Task<IActionResult> UpdateIOTDeviceStatus(UpdateStatusDeviceDto input)
        {
            try
            {
                var result = await _deviceManagementAppService.UpdateDeviceStatusAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating status", error = ex.Message });
            }
        }

        [HttpPost("DeleteIOTDevice")]
        public async Task<IActionResult> DeleteIOTDevice(UpdateStatusDeviceDto input)
        {
            try
            {
                var result = await _deviceManagementAppService.DeleteDeviceAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to delete IOT device", error = ex.Message });
            }
        }
        #endregion  ================================ Device Management ================================
    }
}
