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
using Morpho.EntityFrameworkCore;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morpho.Vehicle
{
    public class VehicleAppService : ApplicationService, IVehicleAppService
    {
        private readonly IRepository<Morpho.Domain.Entities.Vehicles.Vehicles, long> _vehicleRepository;
      //  private readonly MorphoDbContext _context;


       // public VehicleAppService(IRepository<Morpho.Domain.Entities.Vehicles.Vehicles, long> vehicleRepository, MorphoDbContext context)
             public VehicleAppService(IRepository<Morpho.Domain.Entities.Vehicles.Vehicles, long> vehicleRepository)
        {
            vehicleRepository = _vehicleRepository;
           // _context = context;
        }

        public async Task<CreateVehicleDto> AddVehicleAsync(CreateVehicleDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            //var exists = await _vehicleRepository.FirstOrDefaultAsync(x =>
            //    x.TenantId == AbpSession.TenantId.Value &&
            //    x.vehicle_number.ToLower() == input.vehicle_number.ToLower() &&
            //    !x.IsDeleted
            //);

            //if (exists != null)
            //{
            //    throw new UserFriendlyException("Vehicle already exists.");
            //}
            var entity = ObjectMapper.Map<Morpho.Domain.Entities.Vehicles.Vehicles>(input);
            entity.vehicle_unqiue_id = input.vehicle_unqiue_id; //await SequentialDeviceIdGenerator.GenerateDeviceIdSequentialAsync(_context, AbpSession.TenantId.Value);
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.isblock = false;
            entity.IsDeleted = false;

            await _vehicleRepository.InsertAsync(entity);

            return input;

        }
        public async Task<List<VehicleDto>> GetVehicleListAsync()
        {
            var list = await _vehicleRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<VehicleDto>>(list);
        }

        public async Task<UpdateVehicleDto> UpdateVehicleAsync(UpdateVehicleDto input)
        {
            var entity = await _vehicleRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle not found");
            }
            var entity1 = await _vehicleRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.vehicle_number.ToLower() == input.vehicle_number.ToLower() &&
                     !x.IsDeleted);
            if (entity1 != null)
            {
                throw new UserFriendlyException("Vehicle already found");
            }

            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _vehicleRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<UpdateStatusVehicleDto> UpdateVehicleStatusAsync(UpdateStatusVehicleDto input)
        {
            var entity = await _vehicleRepository.FirstOrDefaultAsync(x => x.Id == input.VehicleId);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle  not found");
            }
            entity.isblock = entity.isblock == true?false:true;
            entity.block_by = AbpSession.UserId;
            entity.block_at = DateTime.Now;
            await _vehicleRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusVehicleDto> DeleteVehicleAsync(UpdateStatusVehicleDto input)
        {
            var entity = await _vehicleRepository.FirstOrDefaultAsync(x => x.Id == input.VehicleId);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _vehicleRepository.UpdateAsync(entity);

            return input;
        }
        public async Task<VehicleDto> GetVehicleDetailsAsync(long vehicleId)
        {
            var entity = await _vehicleRepository
                .FirstOrDefaultAsync(x => x.Id == vehicleId);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle not found");
            }
            return ObjectMapper.Map<VehicleDto>(entity);
        }
        public static class SequentialVehicleGenerator
        {
            private static readonly Random _random = new();

            public static async Task<string> GenerateVehicleIdSequentialAsync(
                MorphoDbContext db,
                int tenantId,
                string prefix = "VCL",
                int digits = 5)
            {
                string suffix = GenerateRandomSuffix(4);

                var lastId = await db.Vehicles
                    .Where(d => d.TenantId == tenantId)
                    .OrderByDescending(d => d.created_at)
                    .Select(d => d.vehicle_unqiue_id)
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
