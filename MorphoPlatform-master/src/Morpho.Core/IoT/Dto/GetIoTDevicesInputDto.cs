using Abp.Application.Services.Dto;

namespace Morpho.IoT.Dto
{
    public class GetIoTDevicesInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
