using Abp;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Specifications;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Morpho.Device.TrackingDevice;
using Morpho.DocsVehicle;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.Devices;
using Morpho.Domain.Entities.VehicleContainer;
using Morpho.Domain.Entities.VehicleDocument;
using Morpho.Domain.Entities.VehicleDocumentType;
using Morpho.EntityFrameworkCore;
using Morpho.VehicleContainer.Container.Dto;
using Morpho.VehicleDocs.DocsVehicle.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morpho.DocsVehicle
{
    public class DocsVehicleAppService : ApplicationService, IDocsVehicleAppService
    {
        private readonly IRepository<VehicleDocument, long> _vehicleDocsRepository;
       // private readonly MorphoDbContext _context;


        public DocsVehicleAppService(IRepository<VehicleDocument, long> vehicleDocsRepository)
        {
            _vehicleDocsRepository= vehicleDocsRepository ;
        }

        public async Task<CreateDocsVehicleDto> AddDocsVehicleAsync(CreateDocsVehicleDto input)
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new UserFriendlyException("Tenant not selected!");
            }
            var exists = await _vehicleDocsRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.document_type_id == Convert.ToInt64(input.document_type_id) &&
                x.document_number == input.document_number &&
                !x.IsDeleted
            );

            if (exists != null)
            {
                throw new UserFriendlyException("Vehicle document number already exists.");
            }
            if (input.expiry_date == input.issue_date)
            {
                throw new UserFriendlyException("expiry date and issue date not equal .");
            }
            var entity = ObjectMapper.Map<VehicleDocument>(input);
            entity.TenantId = AbpSession.TenantId.Value;
            entity.created_by = AbpSession.UserId;
            entity.created_at = DateTime.UtcNow;
            entity.is_active = true;
            entity.IsDeleted = false;
            await _vehicleDocsRepository.InsertAsync(entity);
            return input;

        }
        public async Task<List<DocsVehicleDto>> GetDocsVehicleListAsync()
        {
            var list = await _vehicleDocsRepository
                .GetAll()
                 .Include(x => x.mainVehicles)
                 .Include(x => x.mainVehicleDocumentType)
                .Where(x => x.TenantId == AbpSession.TenantId.Value && !x.IsDeleted)
                .OrderByDescending(x => x.created_at)
                .ToListAsync();
            return ObjectMapper.Map<List<DocsVehicleDto>>(list);
        }

        public async Task<UpdateDocsVehicleDto> UpdateDocsVehicleAsync(UpdateDocsVehicleDto input)
        {
         
            input.document_number = input.document_number?.Trim();
            input.document_docs_url = input.document_docs_url?.Trim();
            input.issue_date = input.issue_date?.Trim();
            input.expiry_date = input.expiry_date?.Trim();
            input.description = input.description?.Trim();

            
            var entity = await _vehicleDocsRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle document not found.");
            }
            var document_docs_url = entity.document_docs_url;
        
            DateTime? issueDate = null;
            DateTime? expiryDate = null;

            if (!string.IsNullOrWhiteSpace(input.issue_date))
            {
                issueDate = Convert.ToDateTime(input.issue_date);
            }
            if (!string.IsNullOrWhiteSpace(input.issue_date))
            {
                expiryDate = Convert.ToDateTime(input.expiry_date);
            }
            if (issueDate.HasValue && expiryDate.HasValue && issueDate.Value == expiryDate.Value)
            {
                throw new UserFriendlyException("Expiry date cannot be the same as issue date.");
            }
            var duplicate = await _vehicleDocsRepository.FirstOrDefaultAsync(x =>
                x.TenantId == AbpSession.TenantId.Value &&
                x.document_type_id == Convert.ToInt64(input.document_type_id )&&
                x.document_number == input.document_number &&
                x.Id != input.Id &&
                !x.IsDeleted
            );

            if (duplicate != null)
            {
                throw new UserFriendlyException("Document number already exists.");
            }
          
            if (string.IsNullOrEmpty(input.document_docs_url))
            {
                input.document_docs_url = document_docs_url;
            }
            ObjectMapper.Map(input, entity);
            
            entity.updated_by = AbpSession.UserId;
            entity.Updated_at = DateTime.Now;

            await _vehicleDocsRepository.UpdateAsync(entity);

            return input; 
        }

        public async Task<UpdateStatusDocsVehicleDto> UpdateDocsVehicleStatusAsync(UpdateStatusDocsVehicleDto input)
        {
            var entity = await _vehicleDocsRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle document not found");
            }
            entity.is_active = entity.is_active == true?false:true;
            entity.statu_updated_by = AbpSession.UserId;
            entity.active_at = DateTime.Now;
            await _vehicleDocsRepository.UpdateAsync(entity);
            return input;
        }

        public async Task<UpdateStatusDocsVehicleDto> DeleteDocsVehicleAsync(UpdateStatusDocsVehicleDto input)
        {
            var entity = await _vehicleDocsRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle document not found");
            }
            entity.IsDeleted = true;
            entity.deleted_at = DateTime.Now;
            entity.deleted_by = AbpSession.UserId;
            await _vehicleDocsRepository.UpdateAsync(entity);
            return input;
        }
    

        public async Task<DocsVehicleDto> GetDocsVehicleDetailsAsync(long vehicleDocsId)
        {
            var entity = await _vehicleDocsRepository
                .FirstOrDefaultAsync(x => x.Id == vehicleDocsId);
            if (entity == null)
            {
                throw new UserFriendlyException("Vehicle Document not found");
            }
            return ObjectMapper.Map<DocsVehicleDto>(entity);
        }
    }

}
