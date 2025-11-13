using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Morpho.Services.TenantContacts.Dtos;

namespace Morpho.Services.TenantContacts
{
    public interface ITenantContactService : IAsyncCrudAppService<TenantContactDto, int, PagedAndSortedResultRequestDto, CreateTenantContactDto, UpdateTenantContactDto>
    { }
}
