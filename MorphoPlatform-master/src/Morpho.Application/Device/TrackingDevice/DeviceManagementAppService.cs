using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using Morpho.Device.TrackingDeviceDto;
using Morpho.Domain.Entities.Devices;
using Morpho.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Device.TrackingDevice
{
    public class DeviceManagementAppService : ApplicationService, IDeviceManagementAppService
    {
        private readonly IRepository<TrackingDevices, long> _deviceRepository;
        private readonly MorphoDbContext _context;

       // public DeviceManagementAppService(IRepository<TrackingDevices, long> deviceRepository, MorphoDbContext context)
        public DeviceManagementAppService(IRepository<TrackingDevices, long> deviceRepository)
        {
            _deviceRepository = deviceRepository;
            //_context = context;
        }

        public async Task<CreateDeviceDto> AddDeviceAsync(CreateDeviceDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _deviceRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.imei_number.ToLower() == input.imei_number.ToLower() &&
                !x.IsDeleted
            );
            var exists1 = await _deviceRepository.FirstOrDefaultAsync(x =>
               x.TenantId == AbpSession.TenantId.Value &&
               x.device_unique_no.ToLower() == input.device_unique_no.ToLower() &&
               !x.IsDeleted
           );
            if (exists != null)
            {
                throw new UserFriendlyException("Device imei number already exists.");
            }
            if (exists1 != null)
            {
                throw new UserFriendlyException("Device unique  number already exists.");
            }
            var entity = ObjectMapper.Map<TrackingDevices>(input);
            entity.device_unique_no = input.device_unique_no;
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.isblock = false;
            entity.IsDeleted = false;
            await _deviceRepository.InsertAsync(entity);
            return input;
        }

        public async Task<List<DeviceDto>> GetDeviceListAsync()
        {
            var list = await _deviceRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<DeviceDto>>(list);
        }

        public async Task<UpdateDeviceDto> UpdateDeviceAsync(UpdateDeviceDto input)
        {
            var entity = await _deviceRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Device not found");
            }
            var entity1 = await _deviceRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.imei_number.ToLower() == input.imei_number.ToLower() &&
                     !x.IsDeleted);
            var entity2 = await _deviceRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.device_unique_no.ToLower() == input.device_unique_no.ToLower() &&
                   !x.IsDeleted);
            if (entity1 != null)
            {
                throw new UserFriendlyException("Device imei number already found");
            }
            if (entity2 != null)
            {
                throw new UserFriendlyException("Device unique no already found");
            }
            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _deviceRepository.UpdateAsync(entity);
            return input;
        }
        public async Task<UpdateStatusDeviceDto> UpdateDeviceStatusAsync(UpdateStatusDeviceDto input)
        {
            var entity = await _deviceRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Device not found");
            }
            entity.isblock = entity.isblock == true ? false : true;
            entity.block_by = AbpSession.UserId;
            entity.block_at = DateTime.Now;
            await _deviceRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<DeviceDto> GetDeviceDetailsAsync(long deviceId)
        {
            var entity = await _deviceRepository
                .FirstOrDefaultAsync(x => x.Id == deviceId);
            if (entity == null)
            {
                throw new UserFriendlyException("Device not found");
            }
            return ObjectMapper.Map<DeviceDto>(entity);
        }

        public async Task<UpdateStatusDeviceDto> DeleteDeviceAsync(UpdateStatusDeviceDto input)
        {
            var entity = await _deviceRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Device not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _deviceRepository.UpdateAsync(entity);
            return input;
        }
       
    }
    public static class SequentialDeviceIdGenerator
    {
        private static readonly Random _random = new();

        public static async Task<string> GenerateDeviceIdSequentialAsync(
            MorphoDbContext db,
            int tenantId,
            string prefix = "IOT",
            int digits = 5)
        {
            string suffix = GenerateRandomSuffix(4);

            var lastId = await db.TrackingDevices
                .Where(d => d.TenantId == tenantId)
                .OrderByDescending(d => d.created_at)
                .Select(d => d.device_unique_no)
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

