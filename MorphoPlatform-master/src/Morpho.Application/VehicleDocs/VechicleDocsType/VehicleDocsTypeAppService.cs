using Abp;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Specifications;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morpho.Device.TrackingDevice;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.Devices;
using Morpho.Domain.Entities.VehicleContainer;
using Morpho.Domain.Entities.VehicleDocumentType;
using Morpho.EntityFrameworkCore;
using Morpho.VehicleContainer.Container.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morpho.VehicleDocsType
{
    public class VehicleDocTypeAppService : ApplicationService, IVehicleDocsTypeAppService
    {
        private readonly IRepository<VehicleDocumentType, long> _vehicleDocsTypeRepository;
        private readonly MorphoDbContext _context;


        public VehicleDocTypeAppService(IRepository<VehicleDocumentType, long> vehicleDocsTypeRepository)
        {
            _vehicleDocsTypeRepository =vehicleDocsTypeRepository ;
        }

        public async Task<CreateVechicleDocsTypeDto> AddVehiclDocsTypeAsync(CreateVechicleDocsTypeDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _vehicleDocsTypeRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.document_type_name.ToLower() == input.document_type_name.ToLower() &&
                !x.IsDeleted
            );

            if (exists != null)
            {
                throw new UserFriendlyException("Vehicle document type already exists.");
            }
            var entity = ObjectMapper.Map<VehicleDocumentType>(input);
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.is_active = true;
            entity.IsDeleted = false;

            await _vehicleDocsTypeRepository.InsertAsync(entity);

            return input;

        }
        public async Task<List<VechicleDocsTypeDto>> GetVehicleDocsTypeListAsync()
        {
            var list = await _vehicleDocsTypeRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<VechicleDocsTypeDto>>(list);
        }

        public async Task<UpdateVechicleDocsTypeDto> UpdateVehicleDocsTypeAsync(UpdateVechicleDocsTypeDto input)
        {
            var entity = await _vehicleDocsTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle not found");
            }
            var entity1 = await _vehicleDocsTypeRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.document_type_name.ToLower() == input.document_type_name.ToLower() &&
                     !x.IsDeleted);
            if (entity1 != null)
            {
                throw new UserFriendlyException("Vehicle document type already found");
            }

            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _vehicleDocsTypeRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<UpdateStatusVechicleDocsTypeDto> UpdateVehicleDocsTypeStatusAsync(UpdateStatusVechicleDocsTypeDto input)
        {
            var entity = await _vehicleDocsTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle document type not found");
            }
            entity.is_active = entity.is_active == true?false:true;
            entity.active_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _vehicleDocsTypeRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusVechicleDocsTypeDto> DeleteVehicleDocsTypeAsync(UpdateStatusVechicleDocsTypeDto input)
        {
            var entity = await _vehicleDocsTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle document type not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _vehicleDocsTypeRepository.UpdateAsync(entity);
            return input;
        }
    

        public async Task<VechicleDocsTypeDto> GetVehicleDocsTypeDetailsAsync(long vehicleDocTypeId)
        {
            var entity = await _vehicleDocsTypeRepository
            .FirstOrDefaultAsync(x => x.Id == vehicleDocTypeId);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle document type  not found");
            }
            return ObjectMapper.Map<VechicleDocsTypeDto>(entity);
        }
    }

}
