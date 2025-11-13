using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Morpho.Domain.Entities;
using Morpho.Services.CompanyTypes.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace Morpho.Services.CompanyTypes
{
    //[AbpAuthorize(PermissionNames.Pages_Roles)]
    public class CompanyTypeAppService : AsyncCrudAppService<CompanyType, CompanyTypeDto, int, PagedAndSortedResultRequestDto, CreateCompanyTypeDto, UpdateCompanyTypeDto>, ICompanyTypeAppService
    {
        private readonly IRepository<CompanyType, int> _repository;

        public CompanyTypeAppService(IRepository<CompanyType, int> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public override async Task<CompanyTypeDto> CreateAsync(CreateCompanyTypeDto input)
        {
            CheckCreatePermission();

            // Normalize for case-insensitive check (adjust for culture if required)
            var nameToCheck = (input.Name ?? string.Empty).ToLowerInvariant();

            var exists = await _repository.GetAll()
                .AnyAsync(c => c.Name.ToLower() == nameToCheck);

            if (exists)
            {
                throw new UserFriendlyException("A company type with this name already exists.");
            }

            return await base.CreateAsync(input);
        }

        public override async Task<CompanyTypeDto> UpdateAsync(UpdateCompanyTypeDto input)
        {
            CheckUpdatePermission();

            var entity = await _repository.FirstOrDefaultAsync(input.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(CompanyType), input.Id);
            }

            var nameToCheck = (input.Name ?? string.Empty).ToLowerInvariant();
            var exists = await _repository.GetAll()
                .Where(c => c.Id != input.Id)
                .AnyAsync(c => c.Name.ToLower() == nameToCheck);

            if (exists)
            {
                throw new UserFriendlyException("A company type with this name already exists.");
            }

            return await base.UpdateAsync(input);
        }
    }
}
