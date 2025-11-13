using Abp.AutoMapper;
using Morpho.Domain.Common;
using Morpho.Domain.Entities;

namespace Morpho.Services.Countries.Dtos
{
    [AutoMapTo(typeof(Country))]
    public class CreateCountryDto
    {
        public string? Code { get; set; }
        public FullName Name { get; set; }
        public string? PhoneCode { get; set; }
    }
}
