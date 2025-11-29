using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Morpho.Authorization;
using Morpho.Controllers;
using Morpho.Dto;
using Morpho.FuelType;
using Morpho.VehicleContainer;
using Morpho.VehicleContainer.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleDocsType;
using Morpho.VehicleType;
using Morpho.VehicleType.Dto;
using Morpho.Web.Models;
using Morpho.Web.Models.Common.Modals;
using Morpho.Web.Models.Device;
using Morpho.Web.Models.Roles;
using Morpho.Web.Models.Shipment;
using Morpho.Web.Models.Vehichle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Morpho.Web.Controllers
{
    [AbpMvcAuthorize]
    public class MasterController : MorphoControllerBase
    {
        //private readonly IVehicleDocsTypeAppService _vehicleDocsTypeService;
        //private readonly HttpClient _http;
        //private readonly string _apiUrl;
        private readonly IVehicleTypeAppService _vehicleTypeService;
        private readonly IVehicleDocsTypeAppService _vehicleDocsTypeService;
        private readonly IFuelTypeAppService _fuelTypeAppService;
        private readonly IVehicleContainerTypeAppService _vehicleContainerTypeApp;

        //   public MasterController(IVehicleDocsTypeAppService vehicleDocsTypeService, IHttpClientFactory httpClientFactory, IOptions<ModalApiBaseUrl> apiOptions)
        public MasterController(IVehicleTypeAppService vehicleTypeService, IVehicleDocsTypeAppService vehicleDocsTypeService, IFuelTypeAppService fuelTypeAppService,
            IVehicleContainerTypeAppService vehicleContainerTypeApp)
        {
            //_apiUrl = apiOptions.Value.BaseUrl;
            //_vehicleDocsTypeService = vehicleDocsTypeService;
            //_http = httpClientFactory.CreateClient("IgnoreSSL");
            //_http.BaseAddress = new Uri(_apiUrl);
            _vehicleTypeService = vehicleTypeService;
            _vehicleDocsTypeService = vehicleDocsTypeService;
            _fuelTypeAppService = fuelTypeAppService;
            _vehicleContainerTypeApp = vehicleContainerTypeApp;
        }

        #region ########################  Document type managment master #####################


        public IActionResult VehicleDocsTypeIndex()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> EditVehicleDocsTypeModal(long id)
        {
            try
            {
                var dto = await _vehicleDocsTypeService.GetVehicleDocsTypeDetailsAsync(id);
                if (dto == null)
                {
                    return Content("Vehicle Document type not found.");
                }
                return PartialView("Partials/_EditVehicleDocsTypeModal", dto);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> getAllVehicleDocsTypeList()
        {
            try
            {
                var dtoList = await _vehicleDocsTypeService.GetVehicleDocsTypeListAsync();
                if (dtoList == null || !dtoList.Any())
                {
                    return Json(new
                    {
                        ERROR = true,
                        MESSAGE = "Vehicle Document type list not found!",
                        data = new object[0]
                    });
                }
                return Json(new { ERROR = false, MESSAGE = "", data = dtoList });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message, data = new object[0] });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> CreateVehicleDocsType(CreateVechicleDocsTypeDto input)
        {
            try
            {

                input.document_type_name = input.document_type_name?.Trim();
                input.description = input.description?.Trim();
                if (string.IsNullOrWhiteSpace(input.document_type_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Document type name is required!" });
                }

                if (string.IsNullOrWhiteSpace(input.description))
                {
                    return Json(new { ERROR = true, MESSAGE = "Remark is required!" });
                }

                var result = await _vehicleDocsTypeService.AddVehiclDocsTypeAsync(input);

                return Json(new { ERROR = false, MESSAGE = "Created Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateVehicleDocsType(UpdateVechicleDocsTypeDto input)
        {
            try
            {
                input.document_type_name = input.document_type_name?.Trim();
                input.description = input.description?.Trim();
                if (string.IsNullOrWhiteSpace(input.document_type_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Document type name is required!" });
                }

                if (string.IsNullOrWhiteSpace(input.description))
                {
                    return Json(new { ERROR = true, MESSAGE = "Remark is required!" });
                }
                var result = await _vehicleDocsTypeService.UpdateVehicleDocsTypeAsync(input);

                return Json(new { ERROR = false, MESSAGE = "Updated Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteVehicleDocsType(UpdateStatusVechicleDocsTypeDto input)
        {
            try
            {
                var result = await _vehicleDocsTypeService.DeleteVehicleDocsTypeAsync(input);

                return Json(new { ERROR = false, MESSAGE = "Deleted Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStatusVehicleDocsType(UpdateStatusVechicleDocsTypeDto input)
        {
            try
            {
                var result = await _vehicleDocsTypeService.UpdateVehicleDocsTypeStatusAsync(input);

                return Json(new { ERROR = false, MESSAGE = "Status Updated Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }
        #endregion  ########################  Document type managment master #####################

        #region ########################  Fuel type managment master #####################


        public IActionResult VehicleFuelTypeIndex()
        {
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> EditVehicleFuelTypeModal(long id)
        {
            try
            {
                var dto = await _fuelTypeAppService.GetFuelTypeDetailsAsync(id);
                if (dto == null)
                {
                    return Content("Vehicle Fuel type not found.");
                }
                return PartialView("Partials/_EditVehicleFuelTypeModal", dto);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> getAllVehicleFuelTypeList()
        {
            try
            {
                var dtoList = await _fuelTypeAppService.GetFuelTypesListAsync();
                if (dtoList == null || !dtoList.Any())
                {
                    return Json(new
                    {
                        ERROR = true,
                        MESSAGE = "Vehicle fuel type list not found!",
                        data = new object[0]
                    });
                }
                return Json(new { ERROR = false, MESSAGE = "", data = dtoList });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message, data = new object[0] });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateVehicleFuelType(CreateFuelTypeDto input)
        {
            try
            {

                input.fuel_type_name = input.fuel_type_name?.Trim();
                input.remark = input.remark?.Trim();
                if (string.IsNullOrWhiteSpace(input.fuel_type_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Fuel type is required!" });
                }

                if (string.IsNullOrWhiteSpace(input.remark))
                {
                    return Json(new { ERROR = true, MESSAGE = "Remark is required!" });
                }

                var result = await _fuelTypeAppService.AddFuelTypeAsync(input);

                return Json(new { ERROR = false, MESSAGE = "Created Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateVehicleFuelType(UpdateFuelTypeDto input)
        {
            try
            {
                input.fuel_type_name = input.fuel_type_name?.Trim();
                input.remark = input.remark?.Trim();
                if (string.IsNullOrWhiteSpace(input.fuel_type_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle fuel type name is required!" });
                }

                if (string.IsNullOrWhiteSpace(input.remark))
                {
                    return Json(new { ERROR = true, MESSAGE = "Remark is required!" });
                }

                var result = await _fuelTypeAppService.UpdateFuelTypeAsync(input);

                return Json(new { ERROR = false, MESSAGE = "Updated Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }


        [HttpPost]
        public async Task<ActionResult> DeleteVehicleFuelType(UpdateStatusFuelTypeDto input)
        {
            try
            {
                var result = await _fuelTypeAppService.DeleteFuelTypeAsync(input);
                return Json(new { ERROR = false, MESSAGE = "Deleted Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStatusVehicleFuelType(UpdateStatusFuelTypeDto input)
        {
            try
            {
                var result = await _fuelTypeAppService.UpdateFuelTypeStatusAsync(input);
                return Json(new { ERROR = false, MESSAGE = "Status Updated Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }

        }
        #endregion  ########################  Fuel  type managment master #####################

        #region ########################  Container type managment master #####################


        public IActionResult VehicleContainerTypeIndex()
        {
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> EditVehicleContainerTypeModal(long id)
        {
            try
            {
                var dto = await _vehicleContainerTypeApp.GetVehicleContainerTypeDetailsAsync(id);
                if (dto == null)
                {
                    return Content("Vehicle Container type not found.");
                }
                return PartialView("Partials/_EditVehicleContainerTypeModal", dto);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> getAllVehicleContainerTypeList()
        {
            try
            {
                var dtoList = await _vehicleContainerTypeApp.GetVehicleContainerTypeListAsync();
                if (dtoList == null || !dtoList.Any())
                {
                    return Json(new
                    {
                        ERROR = true,
                        MESSAGE = "Vehicle Container type list not found!",
                        data = new object[0]
                    });
                }
                return Json(new { ERROR = false, MESSAGE = "", data = dtoList });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message, data = new object[0] });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateVehicleContainerType(CreateContainerTypeDto input)
        {
            try
            {

                input.container_type = input.container_type?.Trim();
                input.remark = input.remark?.Trim();
                if (string.IsNullOrWhiteSpace(input.container_type))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle Container type is required!" });
                }

                if (string.IsNullOrWhiteSpace(input.remark))
                {
                    return Json(new { ERROR = true, MESSAGE = "Remark is required!" });
                }

                var result = await _vehicleContainerTypeApp.AddVehicleContainerTypeAsync(input);

                return Json(new { ERROR = false, MESSAGE = "Created Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }
        public async Task<PartialViewResult> CreateModal()
        {

            return PartialView("Partials/_CreateVehicleContainerTypeModal", new VehicleContainerTypeViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> UpdateVehicleContainerType(UpdateContainerTypeDto input)
        {
            try
            {
                input.container_type = input.container_type?.Trim();
                input.remark = input.remark?.Trim();
                if (string.IsNullOrWhiteSpace(input.container_type))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle Container type name is required!" });
                }

                if (string.IsNullOrWhiteSpace(input.remark))
                {
                    return Json(new { ERROR = true, MESSAGE = "Remark is required!" });
                }

                var result = await _vehicleContainerTypeApp.UpdateVehicleContainerTypeAsync(input);

                return Json(new { ERROR = false, MESSAGE = "Updated Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }


        [HttpPost]
        public async Task<ActionResult> DeleteVehicleContainerType(UpdateStatusContainerTypeDto input)
        {
            try
            {
                var result = await _vehicleContainerTypeApp.DeleteVehicleContainerTypeAsync(input);
                return Json(new { ERROR = false, MESSAGE = "Deleted Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStatusVehicleContainerType(UpdateStatusContainerTypeDto input)
        {
            try
            {
                var result = await _vehicleContainerTypeApp.UpdateVehicleContainerTypeStatusAsync(input);
                return Json(new { ERROR = false, MESSAGE = "Status Updated Successfully!" });
            }
            catch (UserFriendlyException ufEx)
            {
                return Json(new { ERROR = true, MESSAGE = ufEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }

        }
        #endregion  ########################  Container  type managment master #####################
    }
}
