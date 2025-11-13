using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Morpho.Services.Industries.Dtos;
using Abp.Domain.Repositories;
using Abp.UI;
using Morpho.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Abp.Domain.Entities;

namespace Morpho.Services.Industries
{
    //[AbpAuthorize(PermissionNames.Pages_Roles)]
    public class IndustryAppService : AsyncCrudAppService<Industry, IndustryDto, int, PagedAndSortedResultRequestDto, CreateIndustryDto, UpdateIndustryDto>, IIndustryAppService
    {
        private readonly IRepository<Industry, int> _repository;

        public IndustryAppService(IRepository<Industry, int> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public override async Task<IndustryDto> CreateAsync(CreateIndustryDto input)
        {
            CheckCreatePermission();

            // Normalize for case-insensitive check (adjust for culture if required)
            var nameToCheck = (input.Name ?? string.Empty).ToLowerInvariant();

            var exists = await _repository.GetAll()
                .AnyAsync(c => c.Name.ToLower() == nameToCheck);

            if (exists)
            {
                throw new UserFriendlyException("An industry with this name already exists.");
            }

            return await base.CreateAsync(input);
        }

        public override async Task<IndustryDto> UpdateAsync(UpdateIndustryDto input)
        {
            CheckUpdatePermission();

            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(Industry), input.Id);
            }

            var nameToCheck = (input.Name ?? string.Empty).ToLowerInvariant();
            var exists = await _repository.GetAll()
                .Where(c => c.Id != input.Id)
                .AnyAsync(c => c.Name.ToLower() == nameToCheck);

            if (exists)
            {
                throw new UserFriendlyException("An industry with this name already exists.");
            }

            return await base.UpdateAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();
            await base.DeleteAsync(input);
        }
    }
}
