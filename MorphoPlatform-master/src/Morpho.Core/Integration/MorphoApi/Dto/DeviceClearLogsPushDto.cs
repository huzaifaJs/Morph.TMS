namespace Morpho.Integration.MorphoApi.Dto
{
    public class DeviceClearLogsPushDto
    {
        public int device_id { get; set; }
        public bool clear { get; set; } = true;
    }
}
