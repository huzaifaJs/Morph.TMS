namespace Morpho.Telemetry.Dto
{
    public class CreateTelemetryRecordDto
    {
        public int tenant_id { get; set; }
        public int morpho_device_id { get; set; }
        public long timestamp { get; set; }
        public double battery_level { get; set; }
        public double temperature { get; set; }
        public double humidity { get; set; }
    }
}
