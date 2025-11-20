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
        [Required]
        [MaxLength(250)]
        public string fuel_type_name { get; set; }

        [Required]
        [MaxLength(600)]
        public string remark { get; set; }
    }

    [AutoMapTo(typeof(Morpho.Domain.Entities.FuelType.FuelType))]
    public class UpdateFuelTypeDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string fuel_type_name { get; set; }

        [Required]
        [MaxLength(600)]
        public string Remark { get; set; }
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
