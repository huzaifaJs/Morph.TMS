using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Morpho.Services.Countries.Dtos;

namespace Morpho.Services.Countries
{
    /// <summary>
    ///  Application service to manage countries.
    /// </summary>
    public interface ICountryAppService : IAsyncCrudAppService<
        CountryDto, int, PagedAndSortedResultRequestDto, CreateCountryDto, UpdateCountryDto>
    { }

}
