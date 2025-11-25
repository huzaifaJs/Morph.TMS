using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Morpho.Domain.Entities.IoT;
using Morpho.Domain.Entities.Events;
using Morpho.Domain.Enums;
using Morpho.Domain.ValueObjects;
using Morpho.Integration.MorphoApi.Dto;

namespace Morpho.Domain.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<DeviceEvent, Guid> _eventRepository;
        private readonly IRepository<IoTDevice, Guid> _deviceRepository;

        public EventService(
            IRepository<DeviceEvent, Guid> eventRepository,
            IRepository<IoTDevice, Guid> deviceRepository)
        {
            _eventRepository = eventRepository;
            _deviceRepository = deviceRepository;
        }

        // --------------------------------------------------------------------
        // 1. Process events triggered during Telemetry push
        // --------------------------------------------------------------------
        public async Task ProcessEventAsync(IoTDevice device, MorphoTelemetryPushDto dto)
        {
            if (dto == null) return;

            // Example: threshold event detection
            if (dto.temperature > 50)   // example rule
            {
                var evt = new DeviceEvent(
                    tenantId: device.TenantId,
                    deviceId: device.Id,
                    eventType: "temperature_threshold",
                    message: $"High temperature detected: {dto.temperature} °C",
                    timestampUtc: DateTime.UtcNow
                );

                await _eventRepository.InsertAsync(evt);
            }

            // You can extend this later for more complex rules
        }

        // --------------------------------------------------------------------
        // 2. Handle direct POST /api/device/event
        // --------------------------------------------------------------------
        public async Task RecordMorphoEventAsync(MorphoEventPushDto dto)
        {
            if (dto == null)
                return;

            // Validate device exists
            var device = await _deviceRepository.FirstOrDefaultAsync(
                d => d.MorphoDeviceId == dto.device_id);

            if (device == null)
                return;

            var evt = new DeviceEvent(
                tenantId: device.TenantId,
                deviceId: device.Id,
                eventType: dto.event_type,
                message: dto.message,
                timestampUtc: DateTimeOffset.FromUnixTimeSeconds(dto.timestamp).UtcDateTime
            );

            await _eventRepository.InsertAsync(evt);
        }
    }
}
