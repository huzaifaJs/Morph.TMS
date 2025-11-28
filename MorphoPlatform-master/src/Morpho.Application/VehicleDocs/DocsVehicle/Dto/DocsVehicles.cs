using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.VehicleDocument;
using Morpho.Domain.Entities.Vehicles;
using Morpho.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleDocs.DocsVehicle.Dto
{
    [AutoMapTo(typeof(VehicleDocument))]
    public class CreateDocsVehicleDto
    {
        private string _documentNumber;
        private string _documentDocsUrl;
        private string _issueDate;
        private string _expiryDate;
        private string _description;

        public string? vehicle_id { get; set; }

        public string? document_type_id { get; set; }

        [MaxLength(250)]
        public string document_number
        {
            get => _documentNumber;
            set => _documentNumber = value?.Trim();
        }

        [MaxLength(600)]
        public string document_docs_url
        {
            get => _documentDocsUrl;
            set => _documentDocsUrl = value?.Trim();
        }

        public string issue_date
        {
            get => _issueDate;
            set => _issueDate = value?.Trim();
        }

        public string expiry_date
        {
            get => _expiryDate;
            set => _expiryDate = value?.Trim();
        }

        [MaxLength(600)]
        public string description
        {
            get => _description;
            set => _description = value?.Trim();
        }
    }



    [AutoMapTo(typeof(VehicleDocument))]
    public class UpdateDocsVehicleDto
    {
        [Required]
        public long Id { get; set; }

        private string _documentNumber;
        private string _documentDocsUrl;
        private string _issueDate;
        private string _expiryDate;
        private string _description;

        public string? vehicle_id { get; set; }

        public string? document_type_id { get; set; }

        [MaxLength(250)]
        public string document_number
        {
            get => _documentNumber;
            set => _documentNumber = value?.Trim();
        }

        [MaxLength(600)]
        public string document_docs_url
        {
            get => _documentDocsUrl;
            set => _documentDocsUrl = value?.Trim();
        }

        public string issue_date
        {
            get => _issueDate;
            set => _issueDate = value?.Trim();
        }

        public string expiry_date
        {
            get => _expiryDate;
            set => _expiryDate = value?.Trim();
        }

        [MaxLength(600)]
        public string description
        {
            get => _description;
            set => _description = value?.Trim();
        }
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
        public long document_type_id { get; set; }
        public long vehicle_id { get; set; }
        public string document_number { get; set; }
        public string document_docs_url { get; set; }
        public DateTime? issue_date { get; set; }
        public DateTime? expiry_date { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public string issue_date_string =>
       issue_date.HasValue ? issue_date.Value.ToString("yyyy-MM-dd") : "";

        public string expiry_date_string =>
            expiry_date.HasValue ? expiry_date.Value.ToString("yyyy-MM-dd") : "";
        public DateTime Created_At { get; set; }
        public long? Created_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public long? Updated_By { get; set; }

        // SAME NAME AS ENTITY
        public VehicleDto mainVehicles { get; set; }
        public VechicleDocsTypeDto mainVehicleDocumentType { get; set; }

        // Computed Fields
        public string vehicle_name => mainVehicles == null
            ? ""
            : $"{mainVehicles.vehicle_unqiue_id} ({mainVehicles.vehicle_name}-{mainVehicles.vehicle_number})";

        public string document_type => mainVehicleDocumentType?.document_type_name;
    }


    public class DocsVehiclePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
