using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Morpho.Services.TenantDocuments.Dtos;

namespace Morpho.Services.TenantDocuments
{
    public interface ITenantDocumentService : IAsyncCrudAppService<TenantDocumentDto, int, PagedAndSortedResultRequestDto, CreateTenantDocumentDto, UpdateTenantDocumentDto>
    { }
}
