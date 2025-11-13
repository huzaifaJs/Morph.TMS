using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Morpho.Services.Industries.Dtos;

namespace Morpho.Services.Industries
{
    public interface IIndustryAppService : IAsyncCrudAppService<
            IndustryDto, int, PagedAndSortedResultRequestDto, CreateIndustryDto, UpdateIndustryDto>
    { }
}
