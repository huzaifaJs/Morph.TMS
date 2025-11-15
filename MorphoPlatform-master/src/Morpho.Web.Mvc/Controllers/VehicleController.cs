using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morpho.Controllers;
using Morpho.VehicleType;
using Morpho.VehicleType.Dto;
using Morpho.Web.Models.Vehichle;
using System.Threading.Tasks;

namespace Morpho.Web.Controllers
{
    [AbpMvcAuthorize]
    public class VehicleController : MorphoControllerBase
    {
        private readonly IVehicleTypeAppService _vehicleTypeService;

        public VehicleController(IVehicleTypeAppService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        public IActionResult VehicleTypeIndex()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateModal()
        {
            return PartialView("_CreateVehicleTypeModal", new VehicleTypeViewModel());
        }
        [HttpGet]
        public async Task<ActionResult> EditModal(long id)
        {
            var dto = await _vehicleTypeService.GetVehicleTypeDetailsAsync(id);

            return PartialView("_EditVehicleTypeModal", dto);
        }

    }
}
