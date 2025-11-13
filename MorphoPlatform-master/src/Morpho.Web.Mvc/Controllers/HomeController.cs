using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Morpho.Controllers;

namespace Morpho.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : MorphoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
