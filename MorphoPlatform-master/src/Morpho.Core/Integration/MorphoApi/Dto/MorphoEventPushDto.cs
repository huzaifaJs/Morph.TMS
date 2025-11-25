namespace Morpho.Integration.MorphoApi.Dto
{
    public class MorphoEventPushDto
    {
        public string severity { get; set; }
        public long timestamp { get; set; }
        public string hostname { get; set; }
        public string application { get; set; }
        public int device_id { get; set; }
        public int event_id { get; set; }
        public string message { get; set; }
        public string event_type { get; set; }
        public EventContext context { get; set; }
    }
}
