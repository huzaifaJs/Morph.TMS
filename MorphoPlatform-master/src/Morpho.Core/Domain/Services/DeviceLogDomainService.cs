using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Morpho.Integration.MorphoApi.Dto;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Entities.Logs;


namespace Morpho.Domain.Services
{
    public class DeviceLogDomainService
    {
        private readonly IRepository<DeviceLog, Guid> _logRepository;

        public DeviceLogDomainService(IRepository<DeviceLog, Guid> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task AppendFromMorphoAsync(IoTDevice device, DeviceLogsResponseDto dto)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.logs == null || dto.logs.Count == 0)
                return;

            var logEntities = new List<DeviceLog>();

            foreach (var entry in dto.logs)
            {
                DateTime timestampUtc = DateTime.UtcNow;

                if (entry.timestamp > 0)
                {
                    try
                    {
                        timestampUtc = DateTimeOffset.FromUnixTimeSeconds(entry.timestamp).UtcDateTime;
                    }
                    catch { }
                }

                var log = new DeviceLog(
                    tenantId: device.TenantId,
                    deviceId: device.Id,
                    severity: entry.severity,
                    message: entry.message,
                    timestampUtc: timestampUtc
                );

                logEntities.Add(log);
            }

            foreach (var log in logEntities)
            {
                await _logRepository.InsertAsync(log);
            }
        }
    }
}
