using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Device.DeviceTypeDto
{
    [AutoMapTo(typeof(Morpho.Domain.Entities.DeviceType))]
    public class CreateDeviceTypeDto
    {
        [Required]
        [MaxLength(250)]
        public string device_type_name { get; set; }

        [Required]
        [MaxLength(600)]
        public string remark { get; set; }
    }

    [AutoMapTo(typeof(Morpho.Domain.Entities.DeviceType))]
    public class UpdateDeviceTypeDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string device_type_name { get; set; }

        [Required]
        [MaxLength(600)]
        public string Remark { get; set; }
    }
    [AutoMapTo(typeof(Morpho.Domain.Entities.DeviceType))]
    public class UpdateStatusDeviceTypeDto
    {
        [Required]
        public long DeviceTypeId { get; set; }
    }

    [AutoMapFrom(typeof(Morpho.Domain.Entities.DeviceType))]
    public class DeviceTypeDto : EntityDto<long>
    {
        public string device_type_name { get; set; }

        public string Remark { get; set; }

        public bool is_active { get; set; }

        public DateTime Created_At { get; set; }

        public long? Created_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public long? Updated_By { get; set; }

    }

    public class DeviceTypePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
