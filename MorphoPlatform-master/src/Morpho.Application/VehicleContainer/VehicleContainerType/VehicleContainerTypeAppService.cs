using Abp;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Specifications;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Morpho.Device.TrackingDevice;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.Devices;
using Morpho.Domain.Entities.VehicleContainer;
using Morpho.Domain.Entities.VehicleDocumentType;
using Morpho.EntityFrameworkCore;
using Morpho.VehicleContainer.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morpho.VehicleContainer
{
    public class VehicleContainerTypeAppService : ApplicationService, IVehicleContainerTypeAppService
    {
        private readonly IRepository<VehicleContainerType, long> _vehicleContainerTypeRepository;


        public VehicleContainerTypeAppService(IRepository<VehicleContainerType, long> vehicleContainerTypeRepository)
        {
            _vehicleContainerTypeRepository= vehicleContainerTypeRepository ;
        }

        public async Task<CreateContainerTypeDto> AddVehicleContainerTypeAsync(CreateContainerTypeDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _vehicleContainerTypeRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.container_type.ToLower() == input.container_type.ToLower() &&
                x.IsDeleted==false
            );

            if (exists != null)
            {
                throw new UserFriendlyException("Vehicle container type already exists.");
            }
            var entity = ObjectMapper.Map<VehicleContainerType>(input);
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.isactive = true;
            entity.IsDeleted = false;

            await _vehicleContainerTypeRepository.InsertAsync(entity);

            return input;

        }
        public async Task<List<ContainerTypeDto>> GetVehicleContainerTypeListAsync()
        {
            var list = await _vehicleContainerTypeRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && x.IsDeleted==false)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<ContainerTypeDto>>(list);
        }

        public async Task<UpdateContainerTypeDto> UpdateVehicleContainerTypeAsync(UpdateContainerTypeDto input)
        {
            var entity = await _vehicleContainerTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle container type not found");
            }
            var entity1 = await _vehicleContainerTypeRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.container_type.ToLower() == input.container_type.ToLower() &&
                     !x.IsDeleted);
            if (entity1 != null)
            {
                throw new UserFriendlyException("Vehicle container type already found");
            }

            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _vehicleContainerTypeRepository.UpdateAsync(entity);
            return input;
        }
        public async Task<UpdateStatusContainerTypeDto> UpdateVehicleContainerTypeStatusAsync(UpdateStatusContainerTypeDto input)
        {
            var entity = await _vehicleContainerTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle container type not found");
            }

            entity.isactive = !entity.isactive;
            entity.active_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _vehicleContainerTypeRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusContainerTypeDto> DeleteVehicleContainerTypeAsync(UpdateStatusContainerTypeDto input)
        {
            var entity = await _vehicleContainerTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle container type not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _vehicleContainerTypeRepository.UpdateAsync(entity);
            return input;
        }
    

        public async Task<ContainerTypeDto> GetVehicleContainerTypeDetailsAsync(long vehicleContainerId)
        {
            var entity = await _vehicleContainerTypeRepository
              .FirstOrDefaultAsync(x => x.Id == vehicleContainerId);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle container Type not found");
            }
            return ObjectMapper.Map<ContainerTypeDto>(entity);
        }
    }

}
