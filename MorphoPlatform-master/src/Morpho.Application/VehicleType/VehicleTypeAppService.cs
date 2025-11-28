using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Specifications;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Morpho.Domain.Entities;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morpho.VehicleType
{
    public class VehicleTypeAppService : ApplicationService, IVehicleTypeAppService
    {
        private readonly IRepository<VehicleTypes, long> _vehicleTypeRepository;

        public VehicleTypeAppService(IRepository<VehicleTypes, long> vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<CreateVehicleTypeDto> AddVehicleTypeAsync(CreateVehicleTypeDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }

            // Check duplicate
            var exists = await _vehicleTypeRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.vehicle_type_name.ToLower() == input.vehicle_type_name.ToLower() &&
                !x.IsDeleted
            );

            if (exists != null)
            {
                throw new UserFriendlyException("Vehicle type already exists.");
            }

            var entity = ObjectMapper.Map<VehicleTypes>(input);

            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.is_active = true;
            entity.IsDeleted = false;

            await _vehicleTypeRepository.InsertAsync(entity);

            return input;

        }



        public async Task<List<VehicleTypeDto>> GetVehicleTypesListAsync()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }

            var tenantId = AbpSession.TenantId.Value;   

            var list = await _vehicleTypeRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<VehicleTypeDto>>(list);
        }

        public async Task<UpdateVehicleTypeDto> UpdateVehicleTypeAsync(UpdateVehicleTypeDto input)
        {
            var entity = await _vehicleTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle Type not found");
            }
            var entity1 = await _vehicleTypeRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.vehicle_type_name.ToLower() == input.vehicle_type_name.ToLower() &&
                     !x.IsDeleted);
            if (entity1 != null)
            {
                throw new UserFriendlyException("Vehicle Type already found");
            }

            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _vehicleTypeRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<UpdateStatusVehicleTypeDto> UpdateVehicleTypeStatusAsync(UpdateStatusVehicleTypeDto input)
        {
            var entity = await _vehicleTypeRepository.FirstOrDefaultAsync(x => x.Id == input.VehicleTypeId);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle Type not found");
            }
            entity.is_active = entity.is_active==true?false:true;
            entity.active_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _vehicleTypeRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusVehicleTypeDto> DeleteVehicleTypeAsync(UpdateStatusVehicleTypeDto input)
        {
            var entity = await _vehicleTypeRepository.FirstOrDefaultAsync(x => x.Id == input.VehicleTypeId);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle Type not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _vehicleTypeRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<VehicleTypeDto> GetVehicleTypeDetailsAsync(long vehicleTypeId)
        {
            var entity = await _vehicleTypeRepository
                .FirstOrDefaultAsync(x => x.Id == vehicleTypeId);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle Type not found");
            }
            return ObjectMapper.Map<VehicleTypeDto>(entity);
        }

        public async Task<List<VehicleTypeDto>> GetVehicleTypesDDListAsync()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }

            var tenantId = AbpSession.TenantId.Value;

            var list = await _vehicleTypeRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted  && x.is_active == true)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<VehicleTypeDto>>(list);
        }
    }

}
