using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.VehicleContainer;
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
        [MaxLength(600)]
        public string remark { get; set; }
           
        [Required]
        [MaxLength(250)]
        public string container_number { get; set; }
          
        [Required]
        public long container_type_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string container_unqiue_id { get; set; }
        public decimal? weight_capacity { get; set; }
        [Required]
        [MaxLength(250)]
        public string ownership { get; set; }
        [Required]
        [MaxLength(250)]
        public string load_status { get; set; } = "Empty";
    }

    [AutoMapTo(typeof(Morpho.Domain.Entities.VehicleContainer.VehicleContainer))]
    public class UpdateContainerDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(600)]
        public string remark { get; set; }

        [Required]
        [MaxLength(250)]
        public string container_number { get; set; }

        [Required]
        public long container_type_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string container_unqiue_id { get; set; }
        public decimal? weight_capacity { get; set; }
        [Required]
        [MaxLength(250)]
        public string ownership { get; set; }
        [Required]
        [MaxLength(250)]
        public string load_status { get; set; } = "Empty";
    }
    [AutoMapTo(typeof(Morpho.Domain.Entities.VehicleContainer.VehicleContainer))]
    public class UpdateStatusContainerDto
    {
        [Required]
        public long Id { get; set; }
    }
    [AutoMapTo(typeof(Morpho.Domain.Entities.VehicleContainer.VehicleContainer))]
    public class ContainerDto : EntityDto<long>
    {
        public string remark { get; set; }
        public string container_number { get; set; }
        public long container_type_id { get; set; }
        public string container_unqiue_id { get; set; }
        public decimal? weight_capacity { get; set; }
        public string ownership { get; set; }
        public string load_status { get; set; } = "Empty";
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
