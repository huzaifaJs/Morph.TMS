using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using Morpho.Device.DeviceTypeDto;
using Morpho.DeviceType;
using Morpho.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Device.DeviceType
{
    public class DeviceTypeAppService : ApplicationService, IDeviceTypeAppService
    {
        private readonly IRepository<Domain.Entities.DeviceType, long> _deviceTypeRepository;

        public DeviceTypeAppService(IRepository<Domain.Entities.DeviceType, long> deviceTypeRepository)
        {
            _deviceTypeRepository = deviceTypeRepository;
        }

        public async Task<CreateDeviceTypeDto> AddDeviceTypeAsync(CreateDeviceTypeDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _deviceTypeRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.device_type_name.ToLower() == input.device_type_name.ToLower() &&
                !x.IsDeleted
            );
            if (exists != null)
            {
                throw new UserFriendlyException("Device type already exists.");
            }
            var entity = ObjectMapper.Map<Domain.Entities.DeviceType>(input);
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.is_active = true;
            entity.IsDeleted = false;
            await _deviceTypeRepository.InsertAsync(entity);
            return input;
        }

        public async Task<List<DeviceTypeDto.DeviceTypeDto>> GetDeviceTypesListAsync()
        {
            var list = await _deviceTypeRepository
                .GetAll()
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<DeviceTypeDto.DeviceTypeDto>>(list);
        }

        public async Task<UpdateDeviceTypeDto> UpdateDeviceTypeAsync(UpdateDeviceTypeDto input)
        {
            var entity = await _deviceTypeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Device Type not found");
            }
            var entity1 = await _deviceTypeRepository.FirstOrDefaultAsync(x => x.Id != input.Id && x.device_type_name.ToLower() == input.device_type_name.ToLower() &&
                     !x.IsDeleted);
            if (entity1 != null)
            {
                throw new UserFriendlyException("Device Type already found");
            }
            ObjectMapper.Map(input, entity);
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;
            await _deviceTypeRepository.UpdateAsync(entity);
            return input;
        }
        public async Task<UpdateStatusDeviceTypeDto> UpdateDeviceTypeStatusAsync(UpdateStatusDeviceTypeDto input)
        {
            var entity = await _deviceTypeRepository.FirstOrDefaultAsync(x => x.Id == input.DeviceTypeId);

            if (entity == null)
            {
                throw new UserFriendlyException("Device Type not found");
            }
            entity.is_active = entity.is_active == true ? false : true;
            entity.active_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _deviceTypeRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<DeviceTypeDto.DeviceTypeDto> GetDeviceTypeDetailsAsync(long deviceTypeId)
        {
            var entity = await _deviceTypeRepository
                .FirstOrDefaultAsync(x => x.Id == deviceTypeId);
            if (entity == null)
            {
                throw new UserFriendlyException("Device Type not found");
            }
            return ObjectMapper.Map<DeviceTypeDto.DeviceTypeDto>(entity);
        }

        public async Task<UpdateStatusDeviceTypeDto> DeleteDeviceTypeAsync(UpdateStatusDeviceTypeDto input)
        {
            var entity = await _deviceTypeRepository.FirstOrDefaultAsync(x => x.Id == input.DeviceTypeId);
            if (entity == null)
            {
                throw new UserFriendlyException("Device Type not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _deviceTypeRepository.UpdateAsync(entity);
            return input;
        }
    }
}
