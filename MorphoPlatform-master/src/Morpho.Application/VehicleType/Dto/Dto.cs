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
        private string _vehicletypename;
        private string _remark;

        [MaxLength(250)]
        public string vehicle_type_name
        {
            get => _vehicletypename;
            set => _vehicletypename = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
       
    }

    [AutoMapTo(typeof(VehicleTypes))]
    public class UpdateVehicleTypeDto
    {
        private string _vehicletypename;
        private string _remark;
        [Required]
        public long Id { get; set; }
        [MaxLength(250)]
        public string vehicle_type_name
        {
            get => _vehicletypename;
            set => _vehicletypename = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
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
