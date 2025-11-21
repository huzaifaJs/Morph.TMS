using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Morpho.Device.TrackingDevice;
using Morpho.EntityFrameworkCore;
using Morpho.ShipmentPackage.Package.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.ShipmentPackage.Package
{
    public class PackageAppService :ApplicationService, IPackageAppService
    {
        private readonly IRepository<Morpho.Domain.Entities.ShipmentPackage.ShipmentPackage, long> _packageRepository;
        public PackageAppService(IRepository<Morpho.Domain.Entities.ShipmentPackage.ShipmentPackage, long> packageRepository)
        {
            _packageRepository= packageRepository;
        }

        public async Task<CreatePackageDto> AddPackageAsync(CreatePackageDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _packageRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.package_number.ToLower() == input.package_number.ToLower() &&
                !x.IsDeleted && x.package_type_id== input.package_type_id
            );
            var exists1 = await _packageRepository.FirstOrDefaultAsync(x =>
              x.TenantId == AbpSession.TenantId.Value &&
              x.package_unique_id.ToLower() == input.package_unique_id.ToLower() &&
              !x.IsDeleted && x.package_type_id == input.package_type_id
          );
            if (exists != null)
            {
                throw new UserFriendlyException("Package number already exists.");
            }
            if (exists1 != null)
            {
                throw new UserFriendlyException("Package unique id already exists.");
            }
            var entity = ObjectMapper.Map<Morpho.Domain.Entities.ShipmentPackage.ShipmentPackage>(input);
            entity.package_unique_id = input.package_unique_id;
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.isactive = false;
            entity.IsDeleted = false;

            await _packageRepository.InsertAsync(entity);

            return input;

        }
        public async Task<List<PackageDto>> GetPackageListAsync()
        {
            var list = await _packageRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<PackageDto>>(list);
        }

        public async Task<UpdatePackageDto> UpdatePackageAsync(UpdatePackageDto input)
        {
            var entity = await _packageRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Package not found");
            }
            var entity1 = await _packageRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.package_number.ToLower() == input.package_number.ToLower() );
            if (entity1 != null)
            {
                throw new UserFriendlyException("Package number already found");
            }
            var entity2 = await _packageRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.package_unique_id.ToLower() == input.package_unique_id.ToLower());
            if (entity2 != null)
            {
                throw new UserFriendlyException("Package unique id already found");
            }
            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _packageRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<UpdateStatusPackageDto> UpdatePackageStatusAsync(UpdateStatusPackageDto input)
        {
            var entity = await _packageRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Package not found");
            }
            entity.isactive = entity.isactive == true ? false : true;
            entity.active_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _packageRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusPackageDto> DeletePackageAsync(UpdateStatusPackageDto input)
        {
            var entity = await _packageRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Package not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _packageRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<PackageDto> GetPackageDetailsAsync(long packageId)
        {
            var entity = await _packageRepository
                .FirstOrDefaultAsync(x => x.Id == packageId);
            if (entity == null)
            {
                throw new UserFriendlyException("Package not found");
            }
            return ObjectMapper.Map<PackageDto>(entity);
        }
        //public static class SequentialPackageGenerator
        //{
        //    private static readonly Random _random = new();

        //    public static async Task<string> GeneratePackageIdSequentialAsync(
        //        MorphoDbContext db,
        //        int tenantId,
        //        string prefix = "PKG",
        //        int digits = 5)
        //    {
        //        string suffix = GenerateRandomSuffix(4);

        //        var lastId = await db.Vehicles
        //            .Where(d => d.TenantId == tenantId)
        //            .OrderByDescending(d => d.created_at)
        //            .Select(d => d.vehicle_unqiue_id)
        //            .FirstOrDefaultAsync();

        //        int next = 1;

        //        if (!string.IsNullOrEmpty(lastId))
        //        {
        //            var parts = lastId.Split('-');
        //            if (parts.Length >= 2 && int.TryParse(parts[1], out int n))
        //                next = n + 1;
        //        }

        //        string serial = next.ToString().PadLeft(digits, '0');
        //        return $"{prefix}-{serial}-{suffix}";
        //    }

        //    private static string GenerateRandomSuffix(int length)
        //    {
        //        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //        var sb = new StringBuilder();
        //        for (int i = 0; i < length; i++)
        //            sb.Append(chars[_random.Next(chars.Length)]);
        //        return sb.ToString();
        //    }
        //}
    }
}
