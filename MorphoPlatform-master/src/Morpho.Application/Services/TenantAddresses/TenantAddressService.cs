using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Morpho.Services.TenantAddresses.Dtos;
using Morpho.Domain.Entities;

namespace Morpho.Services.TenantAddresses
{
    //[AbpAuthorize(PermissionNames.Pages_Roles)]
    public class TenantAddressService : AsyncCrudAppService<TenantAddress, TenantAddressDto, int, PagedAndSortedResultRequestDto, CreateTenantAddressDto, UpdateTenantAddressDto>, ITenantAddressService
    {
        private readonly IRepository<TenantAddress, int> _repository;

        public TenantAddressService(IRepository<TenantAddress, int> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
