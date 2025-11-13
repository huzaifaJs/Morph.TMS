using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Morpho.Services.TenantAddresses.Dtos;

namespace Morpho.Services.TenantAddresses
{
    public interface ITenantAddressService : IAsyncCrudAppService<TenantAddressDto, int, PagedAndSortedResultRequestDto, CreateTenantAddressDto, UpdateTenantAddressDto>
    { }
}
