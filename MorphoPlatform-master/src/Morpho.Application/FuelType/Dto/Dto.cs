using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.FuelType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Dto
{
    [AutoMapTo(typeof(Morpho.Domain.Entities.FuelType.FuelType))]
    public class CreateFuelTypeDto
    {
        private string _fueltypename;
        private string _remark;

        [MaxLength(250)]
        public string fuel_type_name
        {
            get => _fueltypename;
            set => _fueltypename = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
 
    }

    [AutoMapTo(typeof(Morpho.Domain.Entities.FuelType.FuelType))]
    public class UpdateFuelTypeDto
    {
        [Required]
        public long Id { get; set; }
        private string _fueltypename;
        private string _remark;

        [MaxLength(250)]
        public string fuel_type_name
        {
            get => _fueltypename;
            set => _fueltypename = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }

    }
    [AutoMapTo(typeof(Morpho.Domain.Entities.FuelType.FuelType))]
    public class UpdateStatusFuelTypeDto
    {
        [Required]
        public long FuelTypeId { get; set; }
    }

    [AutoMapFrom(typeof(Morpho.Domain.Entities.FuelType.FuelType))]
    public class FuelTypeDto : EntityDto<long>
    {

        public string fuel_type_name { get; set; }

        public string Remark { get; set; }

        public bool is_active { get; set; }

        public DateTime Created_At { get; set; }

        public long? Created_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public long? Updated_By { get; set; }

    }

    public class FuelTypePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
