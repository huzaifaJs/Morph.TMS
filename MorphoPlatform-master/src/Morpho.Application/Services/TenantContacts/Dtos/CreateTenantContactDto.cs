using Abp.AutoMapper;
using Morpho.Domain.Common;
using Morpho.Domain.Entities;

namespace Morpho.Services.TenantContacts.Dtos
{
    [AutoMapTo(typeof(TenantContact))]
    public class CreateTenantContactDto
    {
        public int TenantId { get; set; }
        public Contact Contact { get; set; }
    }
}
