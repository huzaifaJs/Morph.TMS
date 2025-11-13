using Microsoft.AspNetCore.Mvc;
using Morpho.Controllers;
using Morpho.Web.Models.Shipment;
using System.Threading.Tasks;
namespace Morpho.Web.Controllers
{
    public class ShipmentController : MorphoControllerBase
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateShipmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: Save to DB via AppService (we will implement later)
            // For now redirect to same page or details.
            return RedirectToAction("Create");
        }

    }
}
