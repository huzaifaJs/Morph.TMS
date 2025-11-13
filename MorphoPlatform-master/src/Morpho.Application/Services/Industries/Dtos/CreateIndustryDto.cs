using Abp.AutoMapper;
using Morpho.Domain.Entities;

namespace Morpho.Services.Industries.Dtos
{
    [AutoMapTo(typeof(Industry))]
    public class CreateIndustryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
