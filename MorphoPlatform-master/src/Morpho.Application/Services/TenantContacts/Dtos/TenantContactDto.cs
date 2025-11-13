using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Common;
using Morpho.Domain.Entities;

namespace Morpho.Services.TenantContacts.Dtos
{
    [AutoMapFrom(typeof(TenantContact))]
    public class TenantContactDto : EntityDto
    {
        public int TenantId { get; set; }
        public Contact Contact { get; set; }
    }
}
