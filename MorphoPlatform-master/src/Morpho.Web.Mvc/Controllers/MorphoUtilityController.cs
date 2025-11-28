using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Morpho.Controllers;
using Morpho.DocsVehicle;
using Morpho.FuelType;
using Morpho.Vehicle;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleDocsType;
using Morpho.VehicleType;
using Morpho.Web.Models.Common.Modals;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Morpho.Web.Controllers
{
    [AbpMvcAuthorize]
    public class MorphoUtilityController : MorphoControllerBase
    {
        private readonly IVehicleTypeAppService _vehicleTypeService;
        private readonly IVehicleAppService _vhicleAppService;
        private readonly IVehicleDocsTypeAppService _vehicleDocsTypeService;
        private readonly IFuelTypeAppService _fuelTypeAppService;
        private readonly IDocsVehicleAppService _docsVehicle;
        private readonly IVehicleDocsTypeAppService _vehicleDocsType;
        public MorphoUtilityController(IVehicleTypeAppService vehicleTypeService,
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
        public async Task<ActionResult> getVehicleTypeDropDown()
        {
            try
            {
                var dtoList = await _vehicleTypeService.GetVehicleTypesDDListAsync();
                var list = dtoList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.vehicle_type_name
                })
                .ToList();
                list.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "--Select Vehicle Type--"
                });

                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "",
                    data = list
                });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message, data = new object[0] });
            }
        }



        public async Task<ActionResult> getFuelTypeDropDown()
        {
            try
            {
                var dtoList = await _fuelTypeAppService.GetFuelTypesDDLListAsync();
                var list = dtoList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.fuel_type_name
                })
                .ToList();
                list.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "--Select Fuel Type--"
                });

                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "",
                    data = list
                });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message, data = new object[0] });
            }
        }
        [HttpPost]
        public async Task<ActionResult> getGenerateVehicleId()
        {
            try
            {
                var id = await _vhicleAppService.GenerateVehicleUniqueIdAsync();
                string vehicleId = id;
                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "",
                    data = vehicleId
                });
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

        public async Task<ActionResult> getVehicleDropDown()
        {
            try
            {
                var result = await _vhicleAppService.GetVehicleDDListAsync();
                var list = result.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.vehicle_name + " - " + x.vehicle_number + " (" + x.vehicle_unqiue_id + ")"
                })
                 .ToList();
                list.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "--Select Vehicle-"
                });
                List<SelectListItem> ddlVehicleType = new List<SelectListItem>();

               
                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "",
                    data = list
                });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message, data = new object[0] });
            }
        }

        public async Task<ActionResult> getVehicleDocsTypDropDown()
        {
            var dtoList = await _vehicleDocsType.GetVehicleDocsTypeListAsync();
            var list = dtoList.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.document_type_name
            })
            .ToList();
            list.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "--Select Document Type--"
            });

            return Json(new
            {
                ERROR = false,
                MESSAGE = "",
                data = list
            });
        }

    }
}
