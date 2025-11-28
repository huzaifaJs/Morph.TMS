using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.VehicleDocumentType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleDocs.VechicleDocsType.Dto
{
    [AutoMapTo(typeof(VehicleDocumentType))]
    public class CreateVechicleDocsTypeDto
    {
        private string _documentTypeName;
        private string _description;

        [MaxLength(250)]
        public string document_type_name
        {
            get => _documentTypeName;
            set => _documentTypeName = value?.Trim();
        }

        [MaxLength(600)]
        public string description
        {
            get => _description;
            set => _description = value?.Trim();
        }
    }

    [AutoMapTo(typeof(VehicleDocumentType))]
    public class UpdateVechicleDocsTypeDto
    {
        [Required]
        public long Id { get; set; }

        private string _documentTypeName;
        private string _description;

        [MaxLength(250)]
        public string document_type_name
        {
            get => _documentTypeName;
            set => _documentTypeName = value?.Trim();
        }

        [MaxLength(600)]
        public string description
        {
            get => _description;
            set => _description = value?.Trim();
        }
    }
    [AutoMapTo(typeof(VehicleDocumentType))]
    public class UpdateStatusVechicleDocsTypeDto
    {
        [Required]
        public long Id { get; set; }
    }

    [AutoMapFrom(typeof(VehicleDocumentType))]
    public class VechicleDocsTypeDto : EntityDto<long>
    {
        public string document_type_name { get; set; }

        public string description { get; set; }

        public bool is_active { get; set; }

        public DateTime Created_At { get; set; }

        public long? Created_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public long? Updated_By { get; set; }

    }

    public class VechicleDocsTypePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
