namespace Morpho.Integration.MorphoApi.Dto

{
    public class DeviceConfigResponseDto
    {
        public int device_id { get; set; }
        public int frequency { get; set; }
        public int sf { get; set; }
        public int txp { get; set; }
        public string endpoint_URL { get; set; }
        public bool debug_enable { get; set; }
        public bool SD_enable { get; set; }
        public bool RFID_enable { get; set; }
        public string threshold_json { get; set; }
    }
}
