using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities;

namespace Morpho.Services.Industries.Dtos
{
    [AutoMapTo(typeof(Industry))]
    public class UpdateIndustryDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
