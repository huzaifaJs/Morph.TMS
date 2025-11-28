using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Morpho.Controllers;
using Morpho.DocsVehicle;
using Morpho.FuelType;
using Morpho.Vehicle;
using Morpho.VehicleDocs.DocsVehicle.Dto;
using Morpho.VehicleDocsType;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpho.Web.Host.Controllers
{
    [Route("api/VehicleApi/")]
    [ApiController]
    public class VehicleApiController : MorphoControllerBase
    {
        private readonly IVehicleAppService _vehicleService;
        private readonly IDocsVehicleAppService _docsVehicle;

        public VehicleApiController(IVehicleAppService vehicleService, IDocsVehicleAppService docsVehicle)
        {
            _vehicleService = vehicleService;
            _docsVehicle = docsVehicle;
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
        public async Task<IActionResult> UpdateVehicleAsync([FromBody] UpdateVehicleDto input)
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

        #region  ================================ Vehicle Docs ================================
        [HttpGet("GetVehicleDocsAll")]
        public async Task<IActionResult> GetVehicleDocsAll()
        {
            try
            {
                var result = await _docsVehicle.GetDocsVehicleListAsync();
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

        [HttpGet("GetVehicleDocs")]
        public async Task<IActionResult> GetVehicleDocs(long id)
        {
            try
            {
                var result = await _docsVehicle.GetDocsVehicleDetailsAsync(id);
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

        [HttpPost("CreateVehicleDocs")]
        public async Task<IActionResult> CreateVehicleDocs([FromBody] CreateDocsVehicleDto input)
        {
            try
            {
                var result = await _docsVehicle.AddDocsVehicleAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to create vehicle document", error = ex.Message });
            }
        }

        [HttpPost("UpdateVehicleDocsAsync")]
        public async Task<IActionResult> UpdateVehicleDocsAsync([FromBody] UpdateDocsVehicleDto input)
        {
            try
            {
                var result = await _docsVehicle.UpdateDocsVehicleAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to update vehicle document", error = ex.Message });
            }
        }

        [HttpPost("UpdateVhicleDocsStatus")]
        public async Task<IActionResult> UpdateVhicleDocsStatus(UpdateStatusDocsVehicleDto input)
        {
            try
            {
                var result = await _docsVehicle.UpdateDocsVehicleStatusAsync(input);
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

        [HttpPost("DeleteVehicleDocs")]
        public async Task<IActionResult> DeleteVehicleDocs(UpdateStatusDocsVehicleDto input)
        {
            try
            {
                var result = await _docsVehicle.DeleteDocsVehicleAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to delete vehicle document ", error = ex.Message });
            }
        }


        #endregion    ================================ Vehicle Docs ================================
    }
}
