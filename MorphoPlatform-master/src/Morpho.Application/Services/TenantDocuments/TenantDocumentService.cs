using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Morpho.Services.TenantDocuments.Dtos;
using Abp.Domain.Repositories;
using Morpho.Domain.Entities;

namespace Morpho.Services.TenantDocuments
{
    //[AbpAuthorize(PermissionNames.Pages_Roles)]
    public class TenantDocumentService : AsyncCrudAppService<TenantDocument, TenantDocumentDto, int, PagedAndSortedResultRequestDto, CreateTenantDocumentDto, UpdateTenantDocumentDto>, ITenantDocumentService
    {
        private readonly IRepository<TenantDocument, int> _repository;

        public TenantDocumentService(IRepository<TenantDocument, int> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
