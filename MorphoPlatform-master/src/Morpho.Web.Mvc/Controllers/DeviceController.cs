using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morpho.Controllers;
using Morpho.Device.TrackingDeviceDto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleDocsType;
using Morpho.Web.Models.Device;
using Morpho.Web.Models.Vehichle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Web.Controllers
{
    [AbpMvcAuthorize]
    public class DeviceController : MorphoControllerBase
    {
        private readonly IVehicleDocsTypeAppService _vehicleDocsTypeService;
        private readonly HttpClient _http;

        public DeviceController(IVehicleDocsTypeAppService vehicleDocsTypeService, IHttpClientFactory httpClientFactory)
        {
            _vehicleDocsTypeService = vehicleDocsTypeService;
            _http = httpClientFactory.CreateClient("IgnoreSSL");
            _http.BaseAddress = new Uri("https://localhost:44311/");
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

                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var response = await _http.GetAsync($"api/DeviceManagement/GetDeviceDetails?id={id}");

                if (!response.IsSuccessStatusCode)
                {
                    return Content("Error fetching vehicle type details.");
                }
                var jsonString = await response.Content.ReadAsStringAsync();
                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);
                var dto = JsonConvert.DeserializeObject<DeviceDto>(
                    hostResponse.result.ToString()
                );

                if (dto == null)
                {
                    return Content("Vehicle Document type not found.");
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
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var response = await _http.GetAsync("api/DeviceManagement/GetDeviceListAll");

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new { ERROR = true, MESSAGE = "Something went wrong!", data = new object[0] });
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);
                var realList = JsonConvert.DeserializeObject<List<DeviceDto>>(
                    hostResponse.result.ToString()
                );

                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "",
                    data = realList
                });
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


                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/DeviceManagement/CreateIOTDeviceRegister", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);
                var createdObj = JsonConvert.DeserializeObject<CreateDeviceDto>(
                    hostResponse.result.ToString()
                );

                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "Created Successfully!",
                    data = createdObj
                });
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
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/DeviceManagement/UpdateIOTDevice", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<UpdateDeviceDto>(
                    hostResponse.result.ToString()
                );

                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "Updated Successfully!",
                    data = createdObj
                });
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

                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/DeviceManagement/DeleteIOTDevice", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<UpdateStatusVechicleDocsTypeDto>(
                    hostResponse.result.ToString()
                );

                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "Created Successfully!",
                    data = createdObj
                });
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
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/DeviceManagement/UpdateIOTDeviceStatus", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);
                var createdObj = JsonConvert.DeserializeObject<UpdateStatusVechicleDocsTypeDto>(
                    hostResponse.result.ToString()
                );

                return Json(new
                {
                    ERROR = false,
                    MESSAGE = "Status Updated Successfully!",
                    data = createdObj
                });
            }
            catch (Exception ex)
            {
                return Json(new { ERROR = true, MESSAGE = ex.Message });
            }
        }
        #endregion  ########################  Document managment master #####################

    }
}
