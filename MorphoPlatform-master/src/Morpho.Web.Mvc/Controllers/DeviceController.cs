using Abp.AspNetCore.Mvc.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Morpho.Controllers;
using Morpho.Device.TrackingDevice;
using Morpho.Device.TrackingDeviceDto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleDocsType;
using Morpho.Web.Models.Common.Modals;
using Morpho.Web.Models.Device;
using Morpho.Web.Models.Vehichle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Web.Controllers
{
    [AbpMvcAuthorize]
    public class DeviceController : MorphoControllerBase
    {
        private readonly IDeviceManagementAppService _deviceManagementAppService;


        public DeviceController(IDeviceManagementAppService deviceManagementAppService, IHttpClientFactory httpClientFactory, IOptions<ModalApiBaseUrl> apiOptions)
        {
            _deviceManagementAppService = deviceManagementAppService;
        }


        #region ########################  Document managment master #####################
        public IActionResult DeviceIndex()
        {
            return View();
        }
        public async Task<PartialViewResult> CreateModal()
        {

            return PartialView("_CreateDeviceModal", new IOTDeviceViewModel());
        }
        [HttpGet]
        public async Task<ActionResult> EditDeviceModal(long id)
        {
            try
            {
                var dto = await _deviceManagementAppService.GetDeviceDetailsAsync(id);
                if (dto == null)
                {
                    return Content("Device not found.");
                }
                return PartialView("Partials/_EditDeviceModal", dto);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> getAllDeviceList()
        {
            try
            {
                var dtoList = await _deviceManagementAppService.GetDeviceListAsync();
                if (dtoList == null || !dtoList.Any())
                {
                    return Json(new
                    {
                        ERROR = true,
                        MESSAGE = "Device list not found!",
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
        public async Task<ActionResult> CreateDevice(CreateDeviceDto input)
        {
            try
            {
                input.device_unique_no = input.device_unique_no?.Trim();
                input.device_name = input.device_name?.Trim();
                input.device_type_name = input.device_type_name?.Trim();
                input.imei_number = input.imei_number?.Trim();
                if (string.IsNullOrWhiteSpace(input.device_unique_no))
                {
                    return Json(new { ERROR = true, MESSAGE = "Device unique no is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.device_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Device name is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.device_type_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Device type is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.imei_number))
                {
                    return Json(new { ERROR = true, MESSAGE = "IMEI number is required!" });
                }
                var result = await _deviceManagementAppService.AddDeviceAsync(input);

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
        public async Task<ActionResult> UpdateDevice(UpdateDeviceDto input)
        {
            try
            {
                input.device_unique_no = input.device_unique_no?.Trim();
                input.device_name = input.device_name?.Trim();
                input.device_type_name = input.device_type_name?.Trim();
                input.imei_number = input.imei_number?.Trim();
                if (string.IsNullOrWhiteSpace(input.device_unique_no))
                {
                    return Json(new { ERROR = true, MESSAGE = "Device unique no is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.device_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Device name is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.device_type_name))
                {
                    return Json(new { ERROR = true, MESSAGE = "Device type is required!" });
                }
                if (string.IsNullOrWhiteSpace(input.imei_number))
                {
                    return Json(new { ERROR = true, MESSAGE = "IMEI number is required!" });
                }
                var result = await _deviceManagementAppService.UpdateDeviceAsync(input);

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
        public async Task<ActionResult> DeleteDevice(UpdateStatusDeviceDto input)
        {
            try
            {
                var result = await _deviceManagementAppService.DeleteDeviceAsync(input);
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
        public async Task<ActionResult> UpdateStatusDevice(UpdateStatusDeviceDto input)
        {
            try
            {
                var result = await _deviceManagementAppService.UpdateDeviceStatusAsync(input);
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

        #endregion  ########################  Document managment master #####################

    }
}
