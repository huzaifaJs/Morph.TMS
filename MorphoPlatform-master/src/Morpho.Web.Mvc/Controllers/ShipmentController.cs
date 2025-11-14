using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morpho.Authorization;
using Morpho.Controllers;
using Morpho.Web.Models.Shipment;
using System.Threading.Tasks;
namespace Morpho.Web.Controllers
{
    public class ShipmentController : MorphoControllerBase
    {
       
        [AbpAuthorize(PermissionNames.Pages_Shipment_Create)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AbpAuthorize(PermissionNames.Pages_Shipment_Create)]
        public IActionResult Create(CreateShipmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }       
            return RedirectToAction("Create");
        }

    }
}
