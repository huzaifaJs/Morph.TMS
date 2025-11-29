using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.VehicleContainer;
using Morpho.Dto;
using Morpho.VehicleContainer.Dto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleContainer.Container.Dto
{
    [AutoMapTo(typeof(Morpho.Domain.Entities.VehicleContainer.VehicleContainer))]
    public class CreateContainerDto
    {
        [Required]
        [MaxLength(250)]
        public string container_number { get; set; }
          
        [Required]
        public string container_type_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string container_unqiue_id { get; set; }
        public string  weight_capacity { get; set; }
        [Required]
        [MaxLength(250)]
        public string ownership { get; set; }
    }

    [AutoMapTo(typeof(Morpho.Domain.Entities.VehicleContainer.VehicleContainer))]
    public class UpdateContainerDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string container_number { get; set; }

        [Required]
        public string container_type_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string container_unqiue_id { get; set; }
        public string weight_capacity { get; set; }
        [Required]
        [MaxLength(250)]
        public string ownership { get; set; }
    }
    [AutoMapTo(typeof(Morpho.Domain.Entities.VehicleContainer.VehicleContainer))]
    public class UpdateStatusContainerDto
    {
        [Required]
        public long Id { get; set; }
    }
    [AutoMapFrom(typeof(Morpho.Domain.Entities.VehicleContainer.VehicleContainer))]
    public class ContainerDto : EntityDto<long>
    {
        public string container_number { get; set; }
        public long container_type_id { get; set; }
        public string container_unqiue_id { get; set; }
        public decimal? weight_capacity { get; set; }
        public string ownership { get; set; }
        public string load_status { get; set; }
        public ContainerTypeDto VehicleDocumentType { get; set; }
        public string container_type => VehicleDocumentType?.container_type;
        public DateTime Created_At { get; set; }

        public long? Created_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public long? Updated_By { get; set; }

    }

    public class ContainerPagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
