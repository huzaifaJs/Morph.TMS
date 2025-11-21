using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.ShipmentPackage.Package.Dto
{
    [AutoMapTo(typeof(Morpho.Domain.Entities.ShipmentPackage.ShipmentPackage))]
    public class CreatePackageDto
    {
        [Required]
        [MaxLength(250)]
        public string package_number { get; set; }
        [Required]
        [MaxLength(250)]
        public string package_unique_id { get; set; }
        [Required]
        public long? package_type_id { get; set; }
        public string package_type { get; set; }
        [Required]
        public decimal? weight_kg { get; set; }
        [Required]
        public decimal? volume_cbm { get; set; }
        [Required]
        [MaxLength(100)]
        public string dimension { get; set; }
        [Required]
        [MaxLength(600)]
        public string remarks { get; set; }
      
    }

    [AutoMapTo(typeof(Morpho.Domain.Entities.ShipmentPackage.ShipmentPackage))]
    public class UpdatePackageDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string package_number { get; set; }
        [Required]
        [MaxLength(250)]
        public string package_unique_id { get; set; }
        [Required]
        public long? package_type_id { get; set; }
        [Required]
        public decimal? weight_kg { get; set; }
        [Required]
        public decimal? volume_cbm { get; set; }
        [Required]
        [MaxLength(100)]
        public string dimension { get; set; }
        [MaxLength(600)]
        public string remarks { get; set; }
    }
    [AutoMapTo(typeof(Morpho.Domain.Entities.ShipmentPackage.ShipmentPackage))]
    public class UpdateStatusPackageDto
    {
        [Required]
        public long Id { get; set; }
    }
    [AutoMapFrom(typeof(Morpho.Domain.Entities.ShipmentPackage.ShipmentPackage))]
    public class PackageDto : EntityDto<long>
    {
        public string package_number { get; set; }
        public string package_unique_id { get; set; }
        public long? package_type_id { get; set; }
        public decimal? weight_kg { get; set; }
        public decimal? volume_cbm { get; set; }
        public string dimension { get; set; }
        public string remarks { get; set; }
        public DateTime created_at { get; set; } 
        public DateTime? Updated_at { get; set; }
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
    }

    public class PackagePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
