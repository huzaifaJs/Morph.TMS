using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Specifications;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Morpho.Device.TrackingDevice;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.Devices;
using Morpho.Domain.Entities.ShipmentPackage;
using Morpho.Domain.Entities.VehicleContainer;
using Morpho.Domain.Entities.VehicleDocumentType;
using Morpho.EntityFrameworkCore;
using Morpho.ShipmentPackage;
using Morpho.VehicleContainer.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morpho.VehicleContainer
{
    public class PackageTypeAppService : ApplicationService, IPackageTypeAppService
    {
        private readonly IRepository<PackageType, long> _packageTypeRepository;
        private readonly MorphoDbContext _context;


        public PackageTypeAppService(IRepository<PackageType, long> packageTypeRepository, MorphoDbContext context)
        {
            packageTypeRepository = _packageTypeRepository;
            _context = context;
        }

        public async Task<CreatePackageTypeDto> AddPackageTypeAsync(CreatePackageTypeDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _packageTypeRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.package_type_name.ToLower() == input.package_type_name.ToLower() &&
                !x.IsDeleted
            );

            if (exists != null)
            {
                throw new UserFriendlyException("Package type already exists.");
            }
            var entity = ObjectMapper.Map<PackageType>(input);
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.isactive = true;
            entity.IsDeleted = false;

            await _packageTypeRepository.InsertAsync(entity);

            return input;

        }
        public async Task<List<PackageTypeDto>> GetPackageTypeListAsync()
        {
            var list = await _packageTypeRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<PackageTypeDto>>(list);
        }

        public async Task<UpdatePackageTypeDto> UpdatePackageTypeAsync(UpdatePackageTypeDto input)
        {
            var entity = await _packageTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Package type not found");
            }
            var entity1 = await _packageTypeRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.package_type_name.ToLower() == input.package_type_name.ToLower() &&
                     !x.IsDeleted);
            if (entity1 != null)
            {
                throw new UserFriendlyException("Package type already found");
            }

            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _packageTypeRepository.UpdateAsync(entity);
            return input;
        }
        public async Task<UpdateStatusPackageTypeDto> UpdatePackageTypeStatusAsync(UpdateStatusPackageTypeDto input)
        {
            var entity = await _packageTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Package type not found");
            }
            entity.isactive = entity.isactive == true?false:true;
            entity.active_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _packageTypeRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusPackageTypeDto> DeletePackageTypeAsync(UpdateStatusPackageTypeDto input)
        {
            var entity = await _packageTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Package type not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _packageTypeRepository.UpdateAsync(entity);
            return input;
        }
    

        public async Task<PackageTypeDto> GetPackageTypeDetailsAsync(long vehicleContainerId)
        {
            var list = await _packageTypeRepository
               .GetAll()
               .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted && x.Id== vehicleContainerId)
               .OrderByDescending(x => x.created_at)
               .ToListAsync();
            return ObjectMapper.Map<PackageTypeDto>(list);
        }
    }

}
