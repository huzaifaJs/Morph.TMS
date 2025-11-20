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

        // ========= AUTH =========
        public async Task<string> GetAccessTokenAsync()
        {
            var response = await _client.PostAsync("/auth", null);
            var content = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(content);
            return obj.access_token;
        }

        private async Task<HttpClient> AuthorizedAsync()
        {
            var token = await GetAccessTokenAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return _client;
        }

        // ========= GET APIs =========

        public async Task<DeviceStatusResponseDto> GetDeviceStatusAsync(int deviceId)
            => await GetAsync<DeviceStatusResponseDto>($"/api/device/status?device_id={deviceId}");

        public async Task<DeviceConfigResponseDto> GetDeviceConfigAsync(int deviceId)
            => await GetAsync<DeviceConfigResponseDto>($"/api/device/config?device_id={deviceId}");

        public async Task<DeviceConfigResponseDto> GetDeviceConfigResponseAsync(int deviceId)
            => await GetAsync<DeviceConfigResponseDto>($"/api/device/config-response?device_id={deviceId}");

        public async Task<DeviceRebootResponseDto> GetDeviceRebootResponseAsync(int deviceId)
            => await GetAsync<DeviceRebootResponseDto>($"/api/device/reboot-response?device_id={deviceId}");

        public async Task<DeviceLogsResponseDto> GetDeviceLogsAsync(int deviceId)
            => await GetAsync<DeviceLogsResponseDto>($"/api/device/logs?device_id={deviceId}");

        public async Task<DeviceClearLogsCommandDto> GetDeviceClearLogsCommandAsync(int deviceId)
            => await GetAsync<DeviceClearLogsCommandDto>($"/api/device/clear-logs?device_id={deviceId}");

        public async Task<DeviceClearLogsResponseDto> GetDeviceClearLogsResponseAsync(int deviceId)
            => await GetAsync<DeviceClearLogsResponseDto>($"/api/device/clear-logs-response?device_id={deviceId}");

        // ========= POST APIs =========

        public async Task PostDeviceStatusAsync(DeviceStatusPushDto dto)
            => await PostAsync("/api/device/status", dto);

        public async Task PostDeviceConfigAsync(DeviceConfigPushDto dto)
            => await PostAsync("/api/device/config", dto);

        public async Task PostDeviceConfigResponseAsync(DeviceConfigResponseDto dto)
            => await PostAsync("/api/device/config-response", dto);

        public async Task PostDeviceRebootAsync(DeviceRebootPushDto dto)
            => await PostAsync("/api/device/reboot", dto);

        public async Task PostDeviceRebootResponseAsync(DeviceRebootResponseDto dto)
            => await PostAsync("/api/device/reboot-response", dto);

        public async Task PostDeviceLogsAsync(DeviceLogsPushDto dto)
            => await PostAsync("/api/device/logs", dto);

        public async Task PostDeviceClearLogsAsync(DeviceClearLogsPushDto dto)
            => await PostAsync("/api/device/clear-logs", dto);

        // ========= INTERNAL HELPERS =========

        private async Task<T> GetAsync<T>(string route)
        {
            var c = await AuthorizedAsync();
            var r = await c.GetAsync(route);
            var json = await r.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        private async Task PostAsync(string route, object body)
        {
            var c = await AuthorizedAsync();
            var json = JsonConvert.SerializeObject(body);

            await c.PostAsync(
                route,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
        }
    }
}
