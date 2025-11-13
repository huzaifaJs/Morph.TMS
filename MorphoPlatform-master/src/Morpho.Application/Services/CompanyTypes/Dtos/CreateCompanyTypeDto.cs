using Abp.AutoMapper;
using Morpho.Domain.Entities;

namespace Morpho.Services.CompanyTypes.Dtos
{
    [AutoMapTo(typeof(CompanyType))]
    public class CreateCompanyTypeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
