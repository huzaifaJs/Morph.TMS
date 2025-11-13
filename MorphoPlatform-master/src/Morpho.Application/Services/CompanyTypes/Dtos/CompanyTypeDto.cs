using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities;

namespace Morpho.Services.CompanyTypes.Dtos
{
    [AutoMapFrom(typeof(CompanyType))]
    public class CompanyTypeDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
