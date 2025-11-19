using System.Collections.Generic;

namespace Morpho.Integration.MorphoApi.Dto
{
    public class DeviceLogsResponseDto
    {
        public int device_id { get; set; }
        public List<DeviceLogEntryDto> logs { get; set; }
    }

    public class DeviceLogEntryDto
    {
        public string severity { get; set; }
        public string message { get; set; }
        public long timestamp { get; set; }
    }
}
