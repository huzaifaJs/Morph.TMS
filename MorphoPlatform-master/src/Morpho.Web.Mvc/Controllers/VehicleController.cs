using Abp.AspNetCore.Mvc.Authorization;
using Abp.UI;
using Castle.Components.DictionaryAdapter.Xml;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Morpho.Controllers;
using Morpho.DocsVehicle;
using Morpho.Vehicle;
using Morpho.VehicleDocs.DocsVehicle.Dto;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType;
using Morpho.VehicleType.Dto;
using Morpho.Web.Models.Common.Modals;
using Morpho.Web.Models.Vehichle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Morpho.Web.Controllers
{
    [AbpMvcAuthorize]
    public class VehicleController : MorphoControllerBase
    {
        private readonly IVehicleTypeAppService _vehicleTypeService;
        private readonly IVehicleAppService _vehicleService;
        private readonly IDocsVehicleAppService _docsVehicle;
        //private readonly HttpClient _http;
        //private readonly string _apiUrl;

        // public VehicleController(IVehicleTypeAppService vehicleTypeService, IHttpClientFactory httpClientFactory, IOptions<ModalApiBaseUrl> apiOptions)
        public VehicleController(IVehicleTypeAppService vehicleTypeService, IVehicleAppService vehicleService, IDocsVehicleAppService docsVehicle)
        {
            _vehicleTypeService = vehicleTypeService;
            _vehicleService = vehicleService;
            _docsVehicle = docsVehicle;
        }
        #region ########################  Vehicle Type managment master #####################
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
            try
            {
                var dto = await _vehicleTypeService.GetVehicleTypeDetailsAsync(id);
                if (dto == null)
                {
                    return Content("Vehicle type not found.");
                }
                return PartialView("_EditVehicleTypeModal", dto);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult> getAllDataModal()
        {
            try
            {
                var dtoList = await _vehicleTypeService.GetVehicleTypesListAsync();
                if (dtoList == null || !dtoList.Any())
                {
                    return Json(new
                    {
                        ERROR = true,
                        MESSAGE = "Vehicle  type list not found!",
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
        public async Task<ActionResult> CreateVehicleType(CreateVehicleTypeDto input)
        {
            try
            {
                input.vehicle_type_name = input.vehicle_type_name?.Trim();
                input.remark = input.remark?.Trim();
                if (string.IsNullOrWhiteSpace(input.vehicle_type_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle type name is required!" });
                }

                if (string.IsNullOrWhiteSpace(input.remark))
                {
                    return Json(new { ERROR = true, MESSAGE = "Remark is required!" });
                }

                var result = await _vehicleTypeService.AddVehicleTypeAsync(input);

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
        public async Task<ActionResult> UpdateVehicleType(UpdateVehicleTypeDto input)
        {
            try
            {
                input.vehicle_type_name = input.vehicle_type_name?.Trim();
                input.remark = input.remark?.Trim();
                if (string.IsNullOrWhiteSpace(input.vehicle_type_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle type name is required!" });
                }

                if (string.IsNullOrWhiteSpace(input.remark))
                {
                    return Json(new { ERROR = true, MESSAGE = "Remark is required!" });
                }

                var result = await _vehicleTypeService.UpdateVehicleTypeAsync(input);

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
        public async Task<ActionResult> DeleteVehicleType(UpdateStatusVehicleTypeDto input)
        {
            try
            {
                var result = await _vehicleTypeService.DeleteVehicleTypeAsync(input);
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
        public async Task<ActionResult> UpdateStatusVehicleType(UpdateStatusVehicleTypeDto input)
        {

            try
            {
                var result = await _vehicleTypeService.UpdateVehicleTypeStatusAsync(input);
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
        #endregion ########################  Vehicle Type managment master #####################
        #region ########################  Vehicle managment master #####################
        public IActionResult VehicleIndex()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateVehicleModal()
        {
            return PartialView("Partials/_CreateVehicleModal", new VehicleViewModel());
        }
        [HttpGet]
        public async Task<ActionResult> EditVehicleModal(long id)
        {
            try
            {
                var dto = await _vehicleService.GetVehicleDetailsAsync(id);
                if (dto == null)
                {
                    return Content("Vehicle not found.");
                }
                return PartialView("Partials/_EditVehicleModal", dto);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult> getAllDataVehicleModal()
        {
            try
            {
                var dtoList = await _vehicleService.GetVehicleListAsync();
                if (dtoList == null || !dtoList.Any())
                {
                    return Json(new
                    {
                        ERROR = true,
                        MESSAGE = "Vehicle list not found!",
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
        public async Task<ActionResult> CreateVehicle(CreateVehicleDto input)
        {
            try
            {
                input.vehicle_types_id = input.vehicle_types_id?.Trim();
                input.fuel_types_id = input.fuel_types_id?.Trim();
                input.vehicle_unqiue_id = input.vehicle_unqiue_id?.Trim();
                input.vehicle_name = input.vehicle_name?.Trim();
                input.vehicle_number = input.vehicle_number?.Trim();
                input.chassis_number = input.chassis_number?.Trim();
                if (string.IsNullOrWhiteSpace(input.vehicle_types_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle type is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.fuel_types_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Fuel type is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.vehicle_unqiue_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle unique id is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.vehicle_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle name is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.vehicle_number))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle Number is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.chassis_number))
                {
                    return Json(new { ERROR = true, MESSAGE = "Chassis Number is required!" });
                }
                var result = await _vehicleService.AddVehicleAsync(input);

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
        public async Task<ActionResult> UpdateVehicle(UpdateVehicleDto input)
        {
            try
            {
                input.vehicle_types_id = input.vehicle_types_id?.Trim();
                input.fuel_types_id = input.fuel_types_id?.Trim();
                input.vehicle_unqiue_id = input.vehicle_unqiue_id?.Trim();
                input.vehicle_name = input.vehicle_name?.Trim();
                input.vehicle_number = input.vehicle_number?.Trim();
                input.chassis_number = input.chassis_number?.Trim();
                if (string.IsNullOrWhiteSpace(input.vehicle_types_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle type is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.fuel_types_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Fuel type is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.vehicle_unqiue_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle unique id is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.vehicle_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle name is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.vehicle_number))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle Number is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.chassis_number))
                {
                    return Json(new { ERROR = true, MESSAGE = "Chassis Number is required!" });
                }
                var result = await _vehicleService.UpdateVehicleAsync(input);

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
        public async Task<ActionResult> DeleteVehicle(UpdateStatusVehicleDto input)
        {
            try
            {
                var result = await _vehicleService.DeleteVehicleAsync(input);
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
        public async Task<ActionResult> UpdateStatusVehicle(UpdateStatusVehicleDto input)
        {
            try
            {
                var result = await _vehicleService.UpdateVehicleStatusAsync(input);
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
        #endregion ########################  Vehicle managment master #####################
        #region ########################  Vehicle Docs managment master #####################
        public IActionResult VehicleDocsIndex()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateVehicleDocsModal()
        {
            return PartialView("Partials/_CreateVehicleDocsModal", new VehicleDocsViewModel());
        }
        [HttpGet]
        public async Task<ActionResult> EditVehicleDocsModal(long id)
        {
            try
            {
                var dto = await _docsVehicle.GetDocsVehicleDetailsAsync(id);
                if (dto == null)
                {
                    return Content("Vehicle document not found.");
                }
                return PartialView("Partials/_EditVehicleDocsModal", dto);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult> getAllDataVehicleDocs()
        {
            try
            {
                var dtoList = await _docsVehicle.GetDocsVehicleListAsync();
                if (dtoList == null || !dtoList.Any())
                {
                    return Json(new
                    {
                        ERROR = true,
                        MESSAGE = "Vehicle document list not found!",
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
        public async Task<ActionResult> CreateVehicleDocs(CreateDocsVehicleDto input, IFormFile fileUpload)
        {
            try
            {
                input.vehicle_id = input.vehicle_id?.Trim();
                input.document_type_id = input.document_type_id?.Trim();
                input.document_number = input.document_number?.Trim();
                input.issue_date = input.issue_date?.Trim();
                input.expiry_date = input.expiry_date?.Trim();
                if (fileUpload == null || fileUpload.Length == 0)
                {
                    return Json(new { ERROR = true, MESSAGE = "Please upload PDF or Image file" });
                }


                if (fileUpload.Length > 1 * 1024 * 1024)
                {
                    return Json(new { ERROR = true, MESSAGE = "File size must not exceed 1 MB." });
                }


                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
                string fileExt = Path.GetExtension(fileUpload.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExt))
                {
                    return Json(new { ERROR = true, MESSAGE = "Only JPG, JPEG, PNG, and PDF files are allowed." });
                }

                var mime = fileUpload.ContentType.ToLower();

                bool isImage = mime.StartsWith("image/");
                bool isPdf = mime == "application/pdf";

                if (!isImage && !isPdf)
                {
                    return Json(new { ERROR = true, MESSAGE = "Invalid file format. Only PDF or Image allowed." });
                }


                string fileUrl = await FileManagement.WriteFiles(fileUpload, "Vehicle", "VehicleDocs");

                input.document_docs_url = fileUrl;
                if (string.IsNullOrWhiteSpace(input.vehicle_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.document_type_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Document type is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.document_number))
                {
                    return Json(new { ERROR = true, MESSAGE = "Document number is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.issue_date))
                {
                    return Json(new { ERROR = true, MESSAGE = "Issue date is required!" });
                }

                var result = await _docsVehicle.AddDocsVehicleAsync(input);

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
        [AllowAnonymous]
        public async Task<ActionResult> UpdateVehicleDocs(UpdateDocsVehicleDto input, IFormFile fileUpload)
        {
            try
            {
                input.vehicle_id = input.vehicle_id?.Trim();
                input.document_type_id = input.document_type_id?.Trim();
                input.document_number = input.document_number?.Trim();
                input.issue_date = input.issue_date?.Trim();
                input.expiry_date = input.expiry_date?.Trim();
                if (fileUpload != null )
                {
                    if (fileUpload.Length > 1 * 1024 * 1024)
                    {
                        return Json(new { ERROR = true, MESSAGE = "File size must not exceed 1 MB." });
                    }

                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
                    string fileExt = Path.GetExtension(fileUpload.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExt))
                    {
                        return Json(new { ERROR = true, MESSAGE = "Only JPG, JPEG, PNG, and PDF files are allowed." });
                    }
                    var mime = fileUpload.ContentType.ToLower();

                    bool isImage = mime.StartsWith("image/");
                    bool isPdf = mime == "application/pdf";

                    if (!isImage && !isPdf)
                    {
                        return Json(new { ERROR = true, MESSAGE = "Invalid file format. Only PDF or Image allowed." });
                    }


                    string fileUrl = await FileManagement.WriteFiles(fileUpload, "Vehicle", "VehicleDocs");

                    input.document_docs_url = fileUrl;
                }
                else
                {
                    input.document_docs_url = "";
                }
                if (string.IsNullOrWhiteSpace(input.vehicle_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Vehicle is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.document_type_id))
                {
                    return Json(new { ERROR = true, MESSAGE = "Document type is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.document_number))
                {
                    return Json(new { ERROR = true, MESSAGE = "Document number is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.issue_date))
                {
                    return Json(new { ERROR = true, MESSAGE = "Issue date is required!" });
                }

                var result = await _docsVehicle.UpdateDocsVehicleAsync(input);

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
        [AllowAnonymous]
        public async Task<ActionResult> DeleteVehicleDocs(UpdateStatusDocsVehicleDto input)
        {
            try
            {
                var result = await _docsVehicle.DeleteDocsVehicleAsync(input);
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
        [AllowAnonymous]
        public async Task<ActionResult> UpdateStatusVehicleDocs(UpdateStatusDocsVehicleDto input)
        {

            try
            {
                var result = await _docsVehicle.UpdateDocsVehicleStatusAsync(input);
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
        #endregion ########################  Vehicle Docs managment master #####################
    }
}
