using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Morpho.Domain.Entities;
using Morpho.Services.TenantContacts.Dtos;
using System.Threading.Tasks;

namespace Morpho.Services.TenantContacts
{
    //[AbpAuthorize(PermissionNames.Pages_Roles)]
    public class TenantContactService : AsyncCrudAppService<TenantContact, TenantContactDto, int, PagedAndSortedResultRequestDto, CreateTenantContactDto, UpdateTenantContactDto>, ITenantContactService
    {
        private readonly IRepository<TenantContact, int> _repository;

        public TenantContactService(IRepository<TenantContact, int> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
