using Microsoft.AspNetCore.Mvc;
using Morpho.Controllers;
using Morpho.Users;
using Morpho.VehicleType;
using Morpho.VehicleType.Dto;
using Morpho.Web.Models.Users;
using Morpho.Web.Models.Vehichle;
using System;
using System.Threading.Tasks;

namespace Morpho.Web.Controllers
{
    public class VehicleController : MorphoControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleController(IVehicleTypeService _vehicleTypeService)
        {
            _vehicleTypeService = _vehicleTypeService;
        }
        #region ##################################################### Vehicle Type #####################################################
        public async Task<IActionResult> VehicleTypeIndex()
        {
            try
            {
                var list = await _vehicleTypeService.GetVehicleTypesListAsync();
                var model = new VehicleTypeViewModel
                {
                    VehicleTypes = list
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error("Error while fetching vehicle types", ex);
                TempData["ErrorMessage"] = "Something went wrong while loading Vehicle Types.";
                return RedirectToAction("Error", "Home");
            }
        }


        #endregion ##################################################### Vehicle Type #####################################################
    }
}
