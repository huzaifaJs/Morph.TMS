using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleType
{
    using Abp;
    using Abp.Application.Services;
    using Abp.Domain.Repositories;
    using Abp.Runtime.Session;
    using Abp.Specifications;
    using Microsoft.EntityFrameworkCore;
    using Morpho.Domain.Entities;
    using Morpho.VehicleType.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class VehicleTypeAppService : ApplicationService, IVehicleTypeService
    {
        private readonly IRepository<VehicleTypes, long> _vehicleTypeRepository;

        public VehicleTypeAppService(IRepository<VehicleTypes, long> vehicleTypeRepository)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<CreateVehicleTypeDto> AddVehicleTypeAsync(CreateVehicleTypeDto input)
        {
            var existing = await _vehicleTypeRepository.FirstOrDefaultAsync(
                x => x.TenantId == AbpSession.TenantId.Value &&
                     x.vehicle_type_name.ToLower() == input.VehicleTypeName.ToLower() &&
                     !x.isdeleted
            );

            if (existing != null)
            {
                throw new AbpException("Vehicle type already exists.");
            }
            var entity = ObjectMapper.Map<VehicleTypes>(input);

            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.TenantId = AbpSession.TenantId.Value;
            entity.isdeleted = false;
            entity.is_active = true;
            await _vehicleTypeRepository.InsertAsync(entity);
            return input;
        }


        public async Task<List<VehicleTypeDto>> GetVehicleTypesListAsync()
        {
            var list = await _vehicleTypeRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.isdeleted)
                .OrderByDescending(x => x.VehicleTypeId)
                .ToListAsync();
            return ObjectMapper.Map<List<VehicleTypeDto>>(list);
        }

        public async Task<UpdateVehicleTypeDto> UpdateVehicleTypeAsync(UpdateVehicleTypeDto input)
        {
            var entity = await _vehicleTypeRepository.FirstOrDefaultAsync(x => x.VehicleTypeId == input.VehicleTypeId);
            if (entity == null)
            {
                throw new AbpException("Vehicle Type not found");
            }
            var entity1 = await _vehicleTypeRepository.FirstOrDefaultAsync(x => x.VehicleTypeId != input.VehicleTypeId && x.vehicle_type_name.ToLower() == input.VehicleTypeName.ToLower() &&
                     !x.isdeleted && x.TenantId == AbpSession.TenantId.Value);
            if (entity == null)
            {
                throw new AbpException("Vehicle Type already found");
            }

            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _vehicleTypeRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<UpdateStatusVehicleTypeDto> UpdateVehicleTypeStatusAsync(UpdateStatusVehicleTypeDto input)
        {
            var entity = await _vehicleTypeRepository.FirstOrDefaultAsync(x => x.VehicleTypeId == input.VehicleTypeId);

            if (entity == null)
            {
                throw new AbpException("Vehicle Type not found");
            }
            entity.is_active = !entity.is_active;
            entity.active_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _vehicleTypeRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusVehicleTypeDto> DeleteVehicleTypeAsync(UpdateStatusVehicleTypeDto input)
        {
            var entity = await _vehicleTypeRepository.FirstOrDefaultAsync(x => x.VehicleTypeId == input.VehicleTypeId);

            if (entity == null)
            {
                throw new AbpException("Vehicle Type not found");
            }
            entity.isdeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _vehicleTypeRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<VehicleTypeDto> GetVehicleTypeDetailsAsync(int vehicleTypeId)
        {
            var entity = await _vehicleTypeRepository
                .FirstOrDefaultAsync(x => x.VehicleTypeId == vehicleTypeId);
            if (entity == null)
            {
                throw new AbpException("Vehicle Type not found");
            }
            return ObjectMapper.Map<VehicleTypeDto>(entity);
        }
    }

}
