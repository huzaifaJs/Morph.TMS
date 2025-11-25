using System.Threading.Tasks;
using Morpho.Domain.Entities.IoT;
using Morpho.Integration.MorphoApi.Dto;

namespace Morpho.Domain.Services
{
    public interface IEventService
    {
        /// <summary>
        /// Processes telemetry events when Morpho pushes a telemetry packet.
        /// </summary>
        Task ProcessEventAsync(IoTDevice device, MorphoTelemetryPushDto dto);

        /// <summary>
        /// Records an incoming event push from the Morpho device (/api/device/event).
        /// </summary>
        Task RecordMorphoEventAsync(MorphoEventPushDto dto);
    }
}
