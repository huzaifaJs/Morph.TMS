using Abp.Application.Services;
using Morpho.MultiTenancy.Dto;

namespace Morpho.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

