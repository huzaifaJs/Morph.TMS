using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Specifications;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Morpho.Device.DeviceTypeDto;
using Morpho.Domain.Entities;
using Morpho.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morpho.FuelType
{
    public class FuelTypeAppService : ApplicationService, IFuelTypeAppService
    {
        private readonly IRepository<Morpho.Domain.Entities.FuelType.FuelType, long> _fuelTypeRepository;

        public FuelTypeAppService(IRepository<Morpho.Domain.Entities.FuelType.FuelType, long> fuelTypeRepository)
        {
            _fuelTypeRepository = fuelTypeRepository;
        }

        public async Task<CreateFuelTypeDto> AddFuelTypeAsync(CreateFuelTypeDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _fuelTypeRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.fuel_type_name.ToLower() == input.fuel_type_name.ToLower() &&
                !x.IsDeleted
            );
            if (exists != null)
            {
                throw new UserFriendlyException("Fuel type already exists.");
            }
            var entity = ObjectMapper.Map<Morpho.Domain.Entities.FuelType.FuelType>(input);
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.is_active = true;
            entity.IsDeleted = false;
            await _fuelTypeRepository.InsertAsync(entity);
            return input;
        }

        public async Task<List<FuelTypeDto>> GetFuelTypesListAsync()
        {
            var list = await _fuelTypeRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<FuelTypeDto>>(list);
        }

        public async Task<UpdateFuelTypeDto> UpdateFuelTypeAsync(UpdateFuelTypeDto input)
        {
            var entity = await _fuelTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Fuel type not found");
            }
            var entity1 = await _fuelTypeRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.fuel_type_name.ToLower() == input.fuel_type_name.ToLower() &&
                     !x.IsDeleted);
            if (entity1 != null)
            {
                throw new UserFriendlyException("Fuel type already found");
            }

            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _fuelTypeRepository.UpdateAsync(entity);
            return input;
        }
        public async Task<UpdateStatusFuelTypeDto> UpdateFuelTypeStatusAsync(UpdateStatusFuelTypeDto input)
        {
            var entity = await _fuelTypeRepository.FirstOrDefaultAsync(x => x.Id == input.FuelTypeId);

            if (entity == null)
            {
                throw new UserFriendlyException("Fuel Type not found");
            }
            entity.is_active = entity.is_active==true?false:true;
            entity.active_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _fuelTypeRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusFuelTypeDto> DeleteFuelTypeAsync(UpdateStatusFuelTypeDto input)
        {
            var entity = await _fuelTypeRepository.FirstOrDefaultAsync(x => x.Id == input.FuelTypeId);

            if (entity == null)
            {
                throw new UserFriendlyException("Fuel Type not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _fuelTypeRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<FuelTypeDto> GetFuelTypeDetailsAsync(long vehicleTypeId)
        {
            var entity = await _fuelTypeRepository
                .FirstOrDefaultAsync(x => x.Id == vehicleTypeId);
            if (entity == null)
            {
                throw new UserFriendlyException("Fuel Type not found");
            }
            return ObjectMapper.Map<FuelTypeDto>(entity);
        }
    }

}
