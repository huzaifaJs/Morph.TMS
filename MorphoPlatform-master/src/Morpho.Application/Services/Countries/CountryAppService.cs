using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Morpho.Services.Countries.Dtos;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.UI;
using Morpho.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Morpho.Services.Countries
{
    /// <summary>
    ///  Application service to manage countries.
    /// </summary>
    public class CountryAppService : AsyncCrudAppService<Country, CountryDto, int, PagedAndSortedResultRequestDto, CreateCountryDto, UpdateCountryDto>, ICountryAppService
    {
        private readonly IRepository<Country, int> _repository;

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public CountryAppService(IRepository<Country, int> repository)
            : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///  Creates a new country.
        /// </summary>
        /// <param name="input"> </param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<CountryDto> CreateAsync(CreateCountryDto input)
        {
            CheckCreatePermission();

            var nameToCheck = (input.Name.NameEn ?? string.Empty).ToLowerInvariant();

            var exists = await (await _repository.GetAllAsync())
                .AnyAsync(c => c.Name.NameEn.ToLower() == nameToCheck);

            if (exists)
                throw new UserFriendlyException("A country with this English name already exists.");

            return await base.CreateAsync(input);
        }

        public override async Task<CountryDto> UpdateAsync(UpdateCountryDto input)
        {
            CheckUpdatePermission();

            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(Country), input.Id);


            var nameToCheck = (input.Name.NameEn ?? string.Empty).ToLowerInvariant();

            var exists = await (await _repository.GetAllAsync())
                .Where(c => c.Id != input.Id)
                .AnyAsync(c => c.Name.NameEn.ToLower() == nameToCheck);

            if (exists)
                throw new UserFriendlyException("A country with this English name already exists.");


            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity);

            return ObjectMapper.Map<CountryDto>(entity);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            await base.DeleteAsync(input);
        }
    }
}
