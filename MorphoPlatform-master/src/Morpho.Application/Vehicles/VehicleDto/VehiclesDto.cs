using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Vehicles.VehicleDto
{
    [AutoMapTo(typeof(Morpho.Domain.Entities.Vehicles.Vehicles))]
    public class CreateVehicleDto
    {
        [Required]
        [MaxLength(250)]
        public string vehicle_type_name { get; set; }

        [MaxLength(600)]
        public string remark { get; set; }
        [Required]
        [MaxLength(250)]
        public string vehicle_unqiue_id { get; set; }
        [Required]
        public long vehicle_types_id { get; set; }
        [Required]
        public long fuel_types_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string vehicle_number { get; set; }
        [Required]
        [MaxLength(250)]
        public string vehicle_name { get; set; }
        [Required]
        [MaxLength(250)]
        public string model_name { get; set; }
        [Required]
        [MaxLength(250)]
        public string manufacturer { get; set; }
        [Required]
        [MaxLength(250)]
        public string model { get; set; }
        public int manufacturing_year { get; set; }
        public string fuel_type { get; set; }
        public string vehicle_type { get; set; }
        [Required]
        [MaxLength(250)]
        public string chassis_number { get; set; }
        [Required]
        [MaxLength(250)]
        public string engine_number { get; set; }
    }

    [AutoMapTo(typeof(Morpho.Domain.Entities.Vehicles.Vehicles))]
    public class UpdateVehicleDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string vehicle_type_name { get; set; }

        [MaxLength(600)]
        public string remark { get; set; }
        [Required]
        [MaxLength(250)]
        public string vehicle_unqiue_id { get; set; }
        [Required]
        public long vehicle_types_id { get; set; }
        [Required]
        public long fuel_types_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string vehicle_number { get; set; }
        [Required]
        [MaxLength(250)]
        public string vehicle_name { get; set; }
        [Required]
        [MaxLength(250)]
        public string model_name { get; set; }
        [Required]
        [MaxLength(250)]
        public string manufacturer { get; set; }
        [Required]
        [MaxLength(250)]
        public string model { get; set; }
        public int manufacturing_year { get; set; }
        public string fuel_type { get; set; }
        public string vehicle_type { get; set; }
        [Required]
        [MaxLength(250)]
        public string chassis_number { get; set; }
        [Required]
        [MaxLength(250)]
        public string engine_number { get; set; }
    }
    [AutoMapTo(typeof(Morpho.Domain.Entities.Vehicles.Vehicles))]
    public class UpdateStatusVehicleDto
    {
        [Required]
        public long VehicleId { get; set; }
    }

    [AutoMapFrom(typeof(Morpho.Domain.Entities.Vehicles.Vehicles))]
    public class VehicleDto : EntityDto<long>
    {
        public string vehicle_type_name { get; set; }
        public string remark { get; set; }

        public string vehicle_unqiue_id { get; set; }
       
        public long vehicle_types_id { get; set; }
  
        public long fuel_types_id { get; set; }
      
        public string vehicle_number { get; set; }

        public string vehicle_name { get; set; }
    
        public string model_name { get; set; }

        public string manufacturer { get; set; }
     
        public string model { get; set; }
        public int manufacturing_year { get; set; }
        public string fuel_type { get; set; }
        public string vehicle_type { get; set; }
   
        public string chassis_number { get; set; }
  
        public string engine_number { get; set; }
        public bool isblock { get; set; }

        public DateTime Created_At { get; set; }

        public long? Created_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public long? Updated_By { get; set; }

    }

    public class VehiclePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
