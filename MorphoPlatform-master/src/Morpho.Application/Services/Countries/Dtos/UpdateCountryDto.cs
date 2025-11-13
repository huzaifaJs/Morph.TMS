using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Common;
using Morpho.Domain.Entities;

namespace Morpho.Services.Countries.Dtos
{
    [AutoMapTo(typeof(Country))]
    public class UpdateCountryDto : EntityDto
    {
        public string? Code { get; set; }
        public FullName Name { get; set; }
        public string? PhoneCode { get; set; }
    }
}
