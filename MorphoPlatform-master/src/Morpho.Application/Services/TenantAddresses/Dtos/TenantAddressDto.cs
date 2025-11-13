using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Common;
using Morpho.Domain.Entities;
using Morpho.Domain.Enums;

namespace Morpho.Services.TenantAddresses.Dtos
{
    [AutoMapFrom(typeof(TenantAddress))]
    public class TenantAddressDto : EntityDto<int>
    {
        public int TenantId { get; set; }
        public string Type { get; set; }
        public Address Address { get; set; }
    }
}
