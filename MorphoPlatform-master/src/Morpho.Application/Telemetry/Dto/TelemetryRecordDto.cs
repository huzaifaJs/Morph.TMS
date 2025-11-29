using Abp.Application.Services.Dto;
using System;

namespace Morpho.Telemetry.Dto
{
    public class TelemetryRecordDto : EntityDto<Guid>
    {
        public long TimestampRaw { get; set; }
        public DateTime TimestampUtc { get; set; }
        public double BatteryLevel { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double MeanVibration { get; set; }
        public double Light { get; set; }
        public string Status { get; set; }
        public Guid DeviceId { get; set; }
    }
}
