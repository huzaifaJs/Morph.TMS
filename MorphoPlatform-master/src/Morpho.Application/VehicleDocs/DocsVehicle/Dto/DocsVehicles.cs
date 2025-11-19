using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.VehicleDocument;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleDocs.DocsVehicle.Dto
{
    [AutoMapTo(typeof(VehicleDocument))]
    public class CreateDocsVehicleDto
    {
        [Required]
        [MaxLength(250)]
        public string vehicle_type_name { get; set; }
        [Required]
        public long document_type_id { get; set; }
        public string document_type { get; set; }
        [Required]
        [MaxLength(250)]
        public string document_number { get; set; }
        [Required]
        [MaxLength(600)]
        public string document_docs_url { get; set; }
        [Required]
        public DateTime? issue_date { get; set; }
        [Required]
        public DateTime? expiry_date { get; set; }
        [MaxLength(600)]
        public string description { get; set; }
        public bool is_active { get; set; } = true;
    }

    [AutoMapTo(typeof(VehicleDocument))]
    public class UpdateDocsVehicleDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string vehicle_type_name { get; set; }
        [Required]
        public long document_type_id { get; set; }
        public string document_type { get; set; }
        [Required]
        [MaxLength(250)]
        public string document_number { get; set; }
        [Required]
        [MaxLength(600)]
        public string document_docs_url { get; set; }
        [Required]
        public DateTime? issue_date { get; set; }
        [Required]
        public DateTime? expiry_date { get; set; }
        [MaxLength(600)]
        public string description { get; set; }
        public bool is_active { get; set; } = true;
    }
    [AutoMapTo(typeof(VehicleDocument))]
    public class UpdateStatusDocsVehicleDto
    {
        [Required]
        public long Id { get; set; }
    }

    [AutoMapFrom(typeof(VehicleDocument))]
    public class DocsVehicleDto : EntityDto<long>
    {
        public string vehicle_type_name { get; set; }
       
        public long document_type_id { get; set; }
        public string document_type { get; set; }

        public string document_number { get; set; }
    
        public string document_docs_url { get; set; }

        public DateTime? issue_date { get; set; }
   
        public DateTime? expiry_date { get; set; }

        public string description { get; set; }
        public bool is_active { get; set; } = true;

        public DateTime Created_At { get; set; }

        public long? Created_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public long? Updated_By { get; set; }

    }

    public class DocsVehiclePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
