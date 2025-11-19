using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Morpho.Integration.MorphoApi.Dto;
using Newtonsoft.Json;

namespace Morpho.Application.Integration.MorphoApi
{
    public class MorphoApiClient : IMorphoApiClient
    {
        private readonly HttpClient _client;

        public MorphoApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://morpho.challengeone.tn/")
            };
        }

        // ---------------- AUTH TOKEN ----------------
        public async Task<string> GetAccessTokenAsync()
        {
            var response = await _client.PostAsync("/auth", null);
            var content = await response.Content.ReadAsStringAsync();

            dynamic obj = JsonConvert.DeserializeObject(content);
            return obj.access_token;
        }

        private async Task<HttpClient> AuthorizedClientAsync()
        {
            var token = await GetAccessTokenAsync();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return _client;
        }

        // ---------------- Morpho GET APIs ----------------
        public async Task<DeviceStatusResponseDto> GetDeviceStatusAsync(string externalDeviceId)
        {
            var c = await AuthorizedClientAsync();
            var r = await c.GetAsync($"/api/device/status?device_id={externalDeviceId}");
            var json = await r.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DeviceStatusResponseDto>(json);
        }

        public async Task<DeviceConfigResponseDto> GetDeviceConfigAsync(string externalDeviceId)
        {
            var c = await AuthorizedClientAsync();
            var r = await c.GetAsync($"/api/device/config?device_id={externalDeviceId}");
            var json = await r.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DeviceConfigResponseDto>(json);
        }

        public async Task<DeviceLogsResponseDto> GetDeviceLogsAsync(string externalDeviceId)
        {
            var c = await AuthorizedClientAsync();
            var r = await c.GetAsync($"/api/device/logs?device_id={externalDeviceId}");
            var json = await r.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DeviceLogsResponseDto>(json);
        }

        // ---------------- Morpho POST APIs ----------------
        public async Task SetDeviceConfigAsync(DeviceConfigPushDto dto)
        {
            var c = await AuthorizedClientAsync();
            var json = JsonConvert.SerializeObject(dto);
            await c.PostAsync(
                "/api/device/config",
                new StringContent(json, Encoding.UTF8, "application/json")
            );
        }

        public async Task SetDeviceRebootAsync(DeviceRebootPushDto dto)
        {
            var c = await AuthorizedClientAsync();
            var json = JsonConvert.SerializeObject(dto);
            await c.PostAsync(
                "/api/device/reboot",
                new StringContent(json, Encoding.UTF8, "application/json")
            );
        }

        public async Task ClearDeviceLogsAsync(DeviceClearLogsPushDto dto)
        {
            var c = await AuthorizedClientAsync();
            var json = JsonConvert.SerializeObject(dto);
            await c.PostAsync(
                "/api/device/clear-logs",
                new StringContent(json, Encoding.UTF8, "application/json")
            );
        }
    }
}
