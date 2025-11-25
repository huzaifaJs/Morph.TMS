using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morpho.Controllers;
using Morpho.FuelType;
using Morpho.Vehicle;
using Morpho.VehicleDocsType;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType;
using Morpho.VehicleType.Dto;
using System;
using System.Threading.Tasks;

namespace Morpho.Web.Host.Controllers
{
    [Route("api/VehicleApi/")]
    [ApiController]
    public class VehicleApiController : MorphoControllerBase
    {
        private readonly IVehicleAppService _vehicleService;

        public VehicleApiController(IVehicleAppService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        #region  ================================ Vehicle ================================
        [HttpGet("GetVehicleAll")]
        public async Task<IActionResult> GetVehicleAll()
        {
            try
            {
                var result = await _vehicleService.GetVehicleListAsync();
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

        [HttpGet("GetVehicle")]
        public async Task<IActionResult> GetVehicle(long id)
        {
            try
            {
                var result = await _vehicleService.GetVehicleDetailsAsync(id);
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

        [HttpPost("CreateVehicle")]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto input)
        {
            try
            {
                var result = await _vehicleService.AddVehicleAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to create vehicle", error = ex.Message });
            }
        }

        [HttpPost("UpdateVehicleAsync")]
        public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleDto input)
        {
            try
            {
                var result = await _vehicleService.UpdateVehicleAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to update vehicle", error = ex.Message });
            }
        }

        [HttpPost("UpdateVhicleStatus")]
        public async Task<IActionResult> UpdateVhicleStatus(UpdateStatusVehicleDto input)
        {
            try
            {
                var result = await _vehicleService.UpdateVehicleStatusAsync(input);
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

        [HttpPost("DeleteVehicle")]
        public async Task<IActionResult> DeleteVehicle(UpdateStatusVehicleDto input)
        {
            try
            {
                var result = await _vehicleService.DeleteVehicleAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to delete vehicle ", error = ex.Message });
            }
        }
        #endregion    ================================ Vehicle Type ================================
    }
}
