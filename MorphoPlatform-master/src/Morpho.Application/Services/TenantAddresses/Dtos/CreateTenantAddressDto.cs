using Abp.AutoMapper;
using Morpho.Domain.Common;
using Morpho.Domain.Entities;
using Morpho.Domain.Enums;

namespace Morpho.Services.TenantAddresses.Dtos
{
    [AutoMapTo(typeof(TenantAddress))]
    public class CreateTenantAddressDto
    {
        public int TenantId { get; set; }
        public string Type { get; set; }
        public Address Address { get; set; }
    }
}
