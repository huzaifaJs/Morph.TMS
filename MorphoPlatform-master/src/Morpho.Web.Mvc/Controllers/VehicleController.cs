using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Morpho.Controllers;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType;
using Morpho.VehicleType.Dto;
using Morpho.Web.Models.Vehichle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private readonly HttpClient _http;

        public VehicleController(IVehicleTypeAppService vehicleTypeService, IHttpClientFactory httpClientFactory)
        {
            _vehicleTypeService = vehicleTypeService;
            _http = httpClientFactory.CreateClient("IgnoreSSL");
            _http.BaseAddress = new Uri("https://localhost:44311/");

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
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var response = await _http.GetAsync($"api/MasterApi/GetVehicleType?id={id}");

                if (!response.IsSuccessStatusCode)
                {
                    return Content("Error fetching vehicle type details.");
                }
                var jsonString = await response.Content.ReadAsStringAsync();
                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);
                var dto = JsonConvert.DeserializeObject<VehicleTypeDto>(
                    hostResponse.result.ToString()
                );

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
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var response = await _http.GetAsync("api/MasterApi/GetVehicleTypeAll");

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new { ERROR = true, MESSAGE = "Something went wrong!", data = new object[0] });
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);
                var realList = JsonConvert.DeserializeObject<List<VehicleTypeDto>>(
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
        public async Task<ActionResult> CreateVehicleType(CreateVehicleTypeDto input)
        {
            try
            {
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/MasterApi/CreateVehicleType", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<VehicleTypeDto>(
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
        public async Task<ActionResult> UpdateVehicleType(UpdateVehicleTypeDto input)
         {
            try
            {
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/MasterApi/UpdateVehicleTypeAsync", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<VehicleTypeDto>(
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
        public async Task<ActionResult> DeleteVehicleType(UpdateStatusVehicleTypeDto input)
        {
            try
            {
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/MasterApi/DeleteVehicleType", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<VehicleTypeDto>(
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
        public async Task<ActionResult> UpdateStatusVehicleType(UpdateStatusVehicleTypeDto input)
        {
            try
            {
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/MasterApi/UpdateVhicleTypeStatus", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<VehicleTypeDto>(
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
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var response = await _http.GetAsync($"api/VehicleApi/GetVehicle?id={id}");

                if (!response.IsSuccessStatusCode)
                {
                    return Content("Error fetching vehicle details.");
                }
                var jsonString = await response.Content.ReadAsStringAsync();
                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);
                var dto = JsonConvert.DeserializeObject<VehicleDto>(
                    hostResponse.result.ToString()
                );

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
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var response = await _http.GetAsync("api/VehicleApi/GetVehicleAll");

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new { ERROR = true, MESSAGE = "Something went wrong!", data = new object[0] });
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);
                var realList = JsonConvert.DeserializeObject<List<VehicleDto>>(
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
        public async Task<ActionResult> CreateVehicle(CreateVehicleDto input)
        {
            try
            {
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/VehicleApi/CreateVehicle", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<CreateVehicleDto>(
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
        public async Task<ActionResult> UpdateVehicle(UpdateVehicleTypeDto input)
        {
            try
            {
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/MasterApi/UpdateVehicleTypeAsync", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<VehicleTypeDto>(
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
        public async Task<ActionResult> DeleteVehicle(UpdateStatusVehicleTypeDto input)
        {
            try
            {
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/MasterApi/DeleteVehicleType", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<VehicleTypeDto>(
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
        public async Task<ActionResult> UpdateStatusVehicle(UpdateStatusVehicleTypeDto input)
        {
            try
            {
                _http.DefaultRequestHeaders.Remove("Abp.TenantId");
                _http.DefaultRequestHeaders.Add("Abp.TenantId", AbpSession.TenantId?.ToString());

                var jsonBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/MasterApi/UpdateVhicleTypeStatus", content);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    dynamic errObj = JsonConvert.DeserializeObject(jsonString);
                    string msg = errObj?.result?.message ?? "Something went wrong!";
                    return Json(new { ERROR = true, MESSAGE = msg });
                }

                dynamic hostResponse = JsonConvert.DeserializeObject(jsonString);

                var createdObj = JsonConvert.DeserializeObject<VehicleTypeDto>(
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
        #endregion ########################  Vehicle managment master #####################
    }
}
