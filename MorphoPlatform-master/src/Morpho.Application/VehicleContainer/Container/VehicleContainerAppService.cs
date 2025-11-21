using Abp;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Specifications;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Morpho.Domain.Entities.VehicleContainer;
using Morpho.EntityFrameworkCore;
using Morpho.VehicleContainer.Container.Dto;
using Morpho.VehicleContainer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morpho.VehicleContainer
{
    public class VehicleContainerAppService : ApplicationService, IVehicleContainerAppService
    {
        private readonly IRepository<Morpho.Domain.Entities.VehicleContainer.VehicleContainer, long> _vehicleContainerRepository;
        private readonly MorphoDbContext _context;


        public VehicleContainerAppService(IRepository<Morpho.Domain.Entities.VehicleContainer.VehicleContainer, long> vehicleContainerRepository)
        {
            _vehicleContainerRepository=vehicleContainerRepository;
        }

        public async Task<CreateContainerDto> AddVehicleContainerAsync(CreateContainerDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _vehicleContainerRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.container_number.ToLower() == input.container_number.ToLower() &&
                !x.IsDeleted
            );

            if (exists != null)
            {
                throw new UserFriendlyException("Vehicle container number already exists.");
            }
            var exists1 = await _vehicleContainerRepository.FirstOrDefaultAsync(x =>
           x.TenantId == AbpSession.TenantId.Value &&
           x.container_unqiue_id.ToLower() == input.container_unqiue_id.ToLower() &&
           !x.IsDeleted
       );

            if (exists1 != null)
            {
                throw new UserFriendlyException("Vehicle container unqiue id already exists.");
            }
            var entity = ObjectMapper.Map<Morpho.Domain.Entities.VehicleContainer.VehicleContainer>(input);
            entity.TenantId = AbpSession.TenantId.Value;
            entity.container_unqiue_id =input.container_unqiue_id ;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.isblock = false;
            entity.IsDeleted = false;

            await _vehicleContainerRepository.InsertAsync(entity);

            return input;

        }
        public async Task<List<ContainerDto>> GetVehicleContainerListAsync()
        {
            var list = await _vehicleContainerRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<ContainerDto>>(list);
        }

        public async Task<UpdateContainerDto> UpdateVehicleContainerAsync(UpdateContainerDto input)
        {
            var entity = await _vehicleContainerRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle container not found");
            }
            var exists = await _vehicleContainerRepository.FirstOrDefaultAsync(x =>
              x.TenantId == AbpSession.TenantId.Value &&
              x.container_number.ToLower() == input.container_number.ToLower() &&
                x.Id != input.Id &&
              !x.IsDeleted
          );
            var exist1 = await _vehicleContainerRepository.FirstOrDefaultAsync(x =>
       x.TenantId == AbpSession.TenantId.Value &&
         x.container_unqiue_id == input.container_unqiue_id &&
         x.Id != input.Id &&
       !x.IsDeleted
   );

            if (exists != null)
            {
                throw new UserFriendlyException("Vehicle container number already exists.");
            }

            if (exist1 != null)
            {
                throw new UserFriendlyException("Vehicle container unqiue id already exists.");
            }
            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _vehicleContainerRepository.UpdateAsync(entity);
            return input;
        }
        public async Task<UpdateStatusContainerDto> UpdateVehicleContainerStatusAsync(UpdateStatusContainerDto input)
        {
            var entity = await _vehicleContainerRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle document not found");
            }
            entity.isblock = entity.isblock == true?false:true;
            entity.block_by = AbpSession.UserId;
            entity.block_at = DateTime.Now;
            await _vehicleContainerRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusContainerDto> DeleteVehicleContainerAsync(UpdateStatusContainerDto input)
        {
            var entity = await _vehicleContainerRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle document  not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _vehicleContainerRepository.UpdateAsync(entity);
            return input;
        }
    

        public async Task<ContainerDto> GetVehicleContainerDetailsAsync(long vehicleContainerId)
        {
            var entity = await _vehicleContainerRepository
              .FirstOrDefaultAsync(x => x.Id == vehicleContainerId);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle container not found");
            }
            return ObjectMapper.Map<ContainerDto>(entity);
        }
        public static class SequentialContainerGenerator
        {
            private static readonly Random _random = new();

            public static async Task<string> GenerateContainerIdSequentialAsync(
                MorphoDbContext db,
                int tenantId,
                string prefix = "CNTR",
                int digits = 5)
            {
                string suffix = GenerateRandomSuffix(4);

                var lastId = await db.VehicleContainer
                    .Where(d => d.TenantId == tenantId)
                    .OrderByDescending(d => d.created_at)
                    .Select(d => d.container_unqiue_id)
                    .FirstOrDefaultAsync();

                int next = 1;

                if (!string.IsNullOrEmpty(lastId))
                {
                    var parts = lastId.Split('-');
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int n))
                        next = n + 1;
                }

                string serial = next.ToString().PadLeft(digits, '0');
                return $"{prefix}-{serial}-{suffix}";
            }

            private static string GenerateRandomSuffix(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var sb = new StringBuilder();
                for (int i = 0; i < length; i++)
                    sb.Append(chars[_random.Next(chars.Length)]);
                return sb.ToString();
            }
        }
    }

}
