using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morpho.Authorization;
using Morpho.Controllers;
using Morpho.VehicleDocsType;
using Morpho.VehicleType;
using Morpho.Web.Models.Shipment;
using Morpho.Web.Models.Vehichle;
using System.Threading.Tasks;

namespace Morpho.Web.Controllers
{
    [AbpMvcAuthorize]
    public class MasterController : MorphoControllerBase
    {
        private readonly IVehicleDocsTypeAppService _vehicleDocsTypeService;

        public MasterController(IVehicleDocsTypeAppService vehicleDocsTypeService)
        {
            _vehicleDocsTypeService = vehicleDocsTypeService;
        }

        #region ########################  Document type managment master #####################


        public IActionResult VehicleDocsTypeIndex()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateVehicleDocsTypeModal()
        {
            return PartialView("_CreateVehicleDocsTypeModal", new VehicleTypeViewModel());
        }
        [HttpGet]
        public async Task<ActionResult> EditVehicleDocsTypeModal(long id)
        {
            var dto = await _vehicleDocsTypeService.GetVehicleDocsTypeDetailsAsync(id);

            return PartialView("Partials/_EditVehicleDocsTypeModal", dto);
        }
        #endregion  ########################  Document type managment master #####################
    }
}
