using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities;

namespace Morpho.Services.CompanyTypes.Dtos
{
    [AutoMapTo(typeof(CompanyType))]
    public class UpdateCompanyTypeDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
