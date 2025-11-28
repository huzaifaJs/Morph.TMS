using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Morpho.Controllers;
using Morpho.DocsVehicle;
using Morpho.FuelType;
using Morpho.Vehicle;
using Morpho.VehicleDocsType;
using Morpho.VehicleType;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpho.Web.Host.Controllers
{
    [Route("api/MorphoUtilityApi/")]
    [ApiController]
    public class MorphoUtilityApiController : MorphoControllerBase
    {
        private readonly IVehicleTypeAppService _vehicleTypeService;
        private readonly IVehicleAppService _vhicleAppService;
        private readonly IVehicleDocsTypeAppService _vehicleDocsTypeService;
        private readonly IFuelTypeAppService _fuelTypeAppService;
        private readonly IDocsVehicleAppService _docsVehicle;
        private readonly IVehicleDocsTypeAppService _vehicleDocsType;

        public MorphoUtilityApiController(IVehicleTypeAppService vehicleTypeService, 
            IVehicleDocsTypeAppService vehicleDocsTypeService,
            IFuelTypeAppService fuelTypeAppService,
             IVehicleAppService vhicleAppService,
             IDocsVehicleAppService docsVehicle,
             IVehicleDocsTypeAppService vehicleDocsType)
        {
            _vehicleTypeService = vehicleTypeService;
            _vehicleDocsTypeService = vehicleDocsTypeService;
            _fuelTypeAppService = fuelTypeAppService;
            _vhicleAppService = vhicleAppService;
            _docsVehicle = docsVehicle;
            _vehicleDocsType = vehicleDocsType;

        }
        [HttpGet("GetVehicleTypeDropDown")]
        public async Task<IActionResult> GetVehicleTypeDropDown()
        {
            try
            {
                var result = await _vehicleTypeService.GetVehicleTypesDDListAsync();

                List<SelectListItem> ddlVehicleType = new List<SelectListItem>();

                foreach (var item in result)
                {
                    ddlVehicleType.Add(new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text = item.vehicle_type_name
                    });
                }

                return Ok(ddlVehicleType);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Unable to load vehicle type dropdown",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetFuelTypeDropDown")]
        public async Task<IActionResult> GetFuelTypeDropDown()
        {
            try
            {
                var result = await _fuelTypeAppService.GetFuelTypesDDLListAsync();

                List<SelectListItem> ddlVehicleType = new List<SelectListItem>();

                foreach (var item in result)
                {
                    ddlVehicleType.Add(new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text = item.fuel_type_name
                    });
                }

                return Ok(ddlVehicleType);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Unable to load vehicle type dropdown",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GenerateVehicleId")]
        public async Task<IActionResult> GenerateVehicleId()
        {
            try
            {
                var id = await _vhicleAppService.GenerateVehicleUniqueIdAsync();

                return Ok(id);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("GetVehicleDocsTypeDropDown")]
        public async Task<IActionResult> GetVehicleDocsTypeDropDown()
        {
            try
            {
                var result = await _vehicleDocsType.GetVehicleDDLDocsTypeListAsync();

                List<SelectListItem> ddlVehicleType = new List<SelectListItem>();

                foreach (var item in result)
                {
                    ddlVehicleType.Add(new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text = item.document_type_name
                    });
                }

                return Ok(ddlVehicleType);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Unable to load vehicle documnet type dropdown",
                    error = ex.Message
                });
            }
        }
        [HttpGet("GetVehicleDropDown")]
        public async Task<IActionResult> GetVehicleDropDown()
        {
            try
            {
                var result = await _vhicleAppService.GetVehicleDDListAsync();

                List<SelectListItem> ddlVehicleType = new List<SelectListItem>();

                foreach (var item in result)
                {
                    ddlVehicleType.Add(new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text = item.vehicle_name+" - "+ item.vehicle_number+" ("+ item.vehicle_unqiue_id+")"
                    });
                }

                return Ok(ddlVehicleType);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Unable to load vehicle type dropdown",
                    error = ex.Message
                });
            }
        }

    }
}
