using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleType.Dto
{

    [AutoMapTo(typeof(VehicleTypes))]
    public class CreateVehicleTypeDto
    {
        [Required]
        [MaxLength(250)]
        public string vehicle_type_name { get; set; }

        [Required]
        [MaxLength(600)]
        public string  remark { get; set; }
    }

    [AutoMapTo(typeof(VehicleTypes))]
    public class UpdateVehicleTypeDto
    {
        [Required]
        public long Id { get; set; }   

        [Required]
        [MaxLength(250)]
        public string vehicle_type_name { get; set; }

        [Required]
        [MaxLength(600)]
        public string Remark { get; set; }
    }
    [AutoMapTo(typeof(VehicleTypes))]
    public class UpdateStatusVehicleTypeDto
    {
        [Required]
        public long VehicleTypeId { get; set; }
    }

    [AutoMapFrom(typeof(VehicleTypes))]
    public class VehicleTypeDto : EntityDto<long>
    {

        public string vehicle_type_name { get; set; }

        public string Remark { get; set; }

        public bool is_active { get; set; }

        public DateTime Created_At { get; set; }

        public long? Created_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public long? Updated_By { get; set; }

    }

    public class VehicleTypePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
