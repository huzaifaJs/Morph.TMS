using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities.ShipmentPackage;
using Morpho.Domain.Entities.VehicleDocumentType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.ShipmentPackage
{
    [AutoMapTo(typeof(PackageType))]
    public class CreatePackageTypeDto
    {
        [Required]
        [MaxLength(250)]
        public string package_type_name { get; set; }

        [Required]
        [MaxLength(600)]
        public string remark { get; set; }
    }

    [AutoMapTo(typeof(PackageType))]
    public class UpdatePackageTypeDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string package_type_name { get; set; }

        [Required]
        [MaxLength(600)]
        public string remark { get; set; }
    }
    [AutoMapTo(typeof(PackageType))]
    public class UpdateStatusPackageTypeDto
    {
        [Required]
        public long Id { get; set; }
    }

    [AutoMapFrom(typeof(PackageType))]
    public class PackageTypeDto : EntityDto<long>
    {
        public string package_type_name { get; set; }

        public string remark { get; set; }

        public bool isactive { get; set; }

        public DateTime Created_At { get; set; }

        public long? Created_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public long? Updated_By { get; set; }

    }

    public class PackageTypePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
