using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Morpho.Controllers;
using Morpho.Dto;
using Morpho.FuelType;
using Morpho.VehicleDocs.DocsVehicle.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleDocsType;
using Morpho.VehicleType;
using Morpho.VehicleType.Dto;
using System;
using System.Threading.Tasks;

namespace Morpho.Web.Host.Controllers
{
    [Route("api/MasterApi/")]
    [ApiController]
    public class VehicleTypeApiController : MorphoControllerBase
    {
        private readonly IVehicleTypeAppService _vehicleTypeService;
        private readonly IVehicleDocsTypeAppService _vehicleDocsTypeService;
        private readonly IFuelTypeAppService _fuelTypeAppService;

        public VehicleTypeApiController(IVehicleTypeAppService vehicleTypeService, IVehicleDocsTypeAppService vehicleDocsTypeService, IFuelTypeAppService fuelTypeAppService)
        {
            _vehicleTypeService = vehicleTypeService;
            _vehicleDocsTypeService = vehicleDocsTypeService;
            _fuelTypeAppService = fuelTypeAppService;
        }
        #region  ================================ Vehicle Type ================================
        [HttpGet("GetVehicleTypeAll")]
        public async Task<IActionResult> GetVehicleTypeAll()
        {
            try
            {
                var result = await _vehicleTypeService.GetVehicleTypesListAsync();
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

        [HttpGet("GetVehicleType")]
        public async Task<IActionResult> GetVehicleType(long id)
        {
            try
            {
                var result = await _vehicleTypeService.GetVehicleTypeDetailsAsync(id);
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

        [HttpPost("CreateVehicleType")]
        public async Task<IActionResult> CreateVehicleType([FromBody] CreateVehicleTypeDto input)
        {
            try
            {
                var result = await _vehicleTypeService.AddVehicleTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to create vehicle type", error = ex.Message });
            }
        }

        [HttpPost("UpdateVehicleTypeAsync")]
        public async Task<IActionResult> UpdateVehicleType([FromBody] UpdateVehicleTypeDto input)
        {
            try
            {
                var result = await _vehicleTypeService.UpdateVehicleTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to update vehicle type", error = ex.Message });
            }
        }

        [HttpPost("UpdateVhicleTypeStatus")]
        public async Task<IActionResult> UpdateVhicleTypeStatus(UpdateStatusVehicleTypeDto input)
        {
            try
            {
                var result = await _vehicleTypeService.UpdateVehicleTypeStatusAsync(input);
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

        [HttpPost("DeleteVehicleType")]
        public async Task<IActionResult> DeleteVehicleType(UpdateStatusVehicleTypeDto input)
        {
            try
            {
                var result = await _vehicleTypeService.DeleteVehicleTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to delete vehicle type", error = ex.Message });
            }
        }
        #endregion    ================================ Vehicle Type ================================


        #region  ================================ Vehicle Docs Type ================================
        [HttpGet("GetVehicleDocsTypeAll")]
        public async Task<IActionResult> GetVehicleDocsTypeAll()
        {
            try
            {
                var result = await _vehicleDocsTypeService.GetVehicleDocsTypeListAsync();
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

        [HttpGet("GetVehicleDocsType")]
        public async Task<IActionResult> GetVehicleDocsType(long id)
        {
            try
            {
                var result = await _vehicleDocsTypeService.GetVehicleDocsTypeDetailsAsync(id);
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

        [HttpPost("CreateVehicleDocsType")]
        public async Task<IActionResult> CreateVehicleDocsType([FromBody] CreateVechicleDocsTypeDto input)
        {
            try
            {
                var result = await _vehicleDocsTypeService.AddVehiclDocsTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to create vehicle document type", error = ex.Message });
            }
        }

        [HttpPost("UpdateVehicleDocsTypeAsync")]
        public async Task<IActionResult> UpdateVehicleDocsType([FromBody]UpdateVechicleDocsTypeDto input)
        {
            try
            {
                var result = await _vehicleDocsTypeService.UpdateVehicleDocsTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to update vehicle document type", error = ex.Message });
            }
        }

        [HttpPost("UpdateVhicleDocsTypeStatus")]
        public async Task<IActionResult> UpdateVhicleDocsTypeStatus(UpdateStatusVechicleDocsTypeDto input)
        {
            try
            {
                var result = await _vehicleDocsTypeService.UpdateVehicleDocsTypeStatusAsync(input);
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

        [HttpPost("DeleteVehicleDocsType")]
        public async Task<IActionResult> DeleteVehicleDocsType(UpdateStatusVechicleDocsTypeDto input)
        {
            try
            {
                var result = await _vehicleDocsTypeService.DeleteVehicleDocsTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to delete vehicle document type", error = ex.Message });
            }
        }
        #endregion    ================================ Vehicle Docs Type ================================

        #region  ================================ Vehicle Fuel Type ================================
        [HttpGet("GetVehicleFuelTypeAll")]
        public async Task<IActionResult> GetVehicleFuelTypeAll()
        {
            try
            {
                var result = await _fuelTypeAppService.GetFuelTypesListAsync();
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

        [HttpGet("GetVehicleFuelType")]
        public async Task<IActionResult> GetVehicleFuelType(long id)
        {
            try
            {
                var result = await _fuelTypeAppService.GetFuelTypeDetailsAsync(id);
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

        [HttpPost("CreateVehicleFuelType")]
        public async Task<IActionResult> CreateVehicleFuelType([FromBody] CreateFuelTypeDto input)
        {
            try
            {
                var result = await _fuelTypeAppService.AddFuelTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to create vehicle fuel type", error = ex.Message });
            }
        }

        [HttpPost("UpdateVehicleFuelTypeAsync")]
        public async Task<IActionResult> UpdateVehicleFuelType([FromBody]UpdateFuelTypeDto input)
        {
            try
            {
                var result = await _fuelTypeAppService.UpdateFuelTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to update vehicle fuel type", error = ex.Message });
            }
        }

        [HttpPost("UpdateVhicleFuelTypeStatus")]
        public async Task<IActionResult> UpdateVhicleFuelypeStatus(UpdateStatusFuelTypeDto input)
        {
            try
            {
                var result = await _fuelTypeAppService.UpdateFuelTypeStatusAsync(input);
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

        [HttpPost("DeleteVehicleFuelType")]
        public async Task<IActionResult> DeleteVehicleFuelType(UpdateStatusFuelTypeDto input)
        {
            try
            {
                var result = await _fuelTypeAppService.DeleteFuelTypeAsync(input);
                return Ok(result);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Unable to delete vehicle fuel type", error = ex.Message });
            }
        }
        #endregion    ================================ Vehicle Docs Type ================================
    }
}
