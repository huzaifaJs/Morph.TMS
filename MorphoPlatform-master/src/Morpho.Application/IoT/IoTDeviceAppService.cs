using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq;
using Abp.Linq.Extensions;
using Morpho.Application.Integration.MorphoApi;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Services;
using Morpho.Domain.Services.Telemetry;
using Morpho.IoT.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;


namespace Morpho.Application.IoT
{
    public class IoTDeviceAppService : ApplicationService, IIoTDeviceAppService
    {
        private readonly IRepository<IoTDevice, Guid> _deviceRepository;
        private readonly IMorphoApiClient _morphoApiClient;
        private readonly TelemetryDomainService _telemetryService;
        private readonly DeviceConfigDomainService _configService;
        private readonly DeviceLogDomainService _logService;

        // 🔥 FIXED: Correct constructor dependency injection
        public IoTDeviceAppService(
            IRepository<IoTDevice, Guid> deviceRepository,
            IMorphoApiClient morphoApiClient,
            TelemetryDomainService telemetryService,
            DeviceConfigDomainService configService,
            DeviceLogDomainService logService)
        {
            _deviceRepository = deviceRepository;
            _morphoApiClient = morphoApiClient;
            _telemetryService = telemetryService;
            _configService = configService;
            _logService = logService;
        }

        // =============================
        // SYNC METHODS
        // =============================

        public async Task SyncStatusAsync(Guid id)
        {
            var device = await _deviceRepository.GetAsync(id);

            var status = await _morphoApiClient.GetDeviceStatusAsync(device.ExternalDeviceId);

            await _telemetryService.RecordStatusFromMorphoAsync(device, status);
        }

        public async Task SyncConfigAsync(Guid id)
        {
            var device = await _deviceRepository.GetAsync(id);

            var config = await _morphoApiClient.GetDeviceConfigAsync(device.ExternalDeviceId);

            await _configService.UpdateFromMorphoAsync(device, config);
        }

        public async Task SyncLogsAsync(Guid id)
        {
            var device = await _deviceRepository.GetAsync(id);

            var logs = await _morphoApiClient.GetDeviceLogsAsync(device.ExternalDeviceId);

            await _logService.AppendFromMorphoAsync(device, logs);
        }

        // =============================
        // CRUD FUNCTIONS
        // =============================

        public async Task<IoTDeviceDto> RegisterAsync(CreateIoTDeviceDto input)
        {
            var device = new IoTDevice(
                input.tenant_id,
                input.external_device_id,
                input.serial_number,
                input.name,
                input.device_type
            );

            var id = await _deviceRepository.InsertAndGetIdAsync(device);

            device = await _deviceRepository.GetAsync(id);
            return device.MapTo<IoTDeviceDto>();
        }

        public async Task<IoTDeviceDto> GetAsync(Guid id)
        {
            var device = await _deviceRepository.GetAsync(id);
            return device.MapTo<IoTDeviceDto>();
        }

        public async Task<PagedResultDto<IoTDeviceDto>> GetListAsync(GetIoTDevicesInputDto input)
        {
            var query = _deviceRepository
                .GetAll()
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                    d => d.Name.Contains(input.Filter) ||
                         d.ExternalDeviceId.Contains(input.Filter));

            // Count
            var totalCount = await query.CountAsync();

            // Paging + Sorting + Fetch
            var items = await query
                .OrderBy(d => d.Name)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<IoTDeviceDto>(
                totalCount,
                items.MapTo<List<IoTDeviceDto>>()
            );
        }

    }
}
