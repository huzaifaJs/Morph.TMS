using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Morpho.Services.CompanyTypes.Dtos;

namespace Morpho.Services.CompanyTypes
{
    public interface ICompanyTypeAppService : IAsyncCrudAppService<
            CompanyTypeDto, int, PagedAndSortedResultRequestDto, CreateCompanyTypeDto, UpdateCompanyTypeDto>
        { }
}
