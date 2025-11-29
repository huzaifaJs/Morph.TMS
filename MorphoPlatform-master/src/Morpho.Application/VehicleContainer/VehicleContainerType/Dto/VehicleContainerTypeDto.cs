using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Morpho.Domain.Entities;
using Morpho.Domain.Entities.VehicleContainer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleContainer.Dto
{
    [AutoMapTo(typeof(VehicleContainerType))]
    public class CreateContainerTypeDto
    {
        private string _containertype;
        private string _remark;

        [MaxLength(250)]
        public string container_type
        {
            get => _containertype;
            set => _containertype = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
       
    }

    [AutoMapTo(typeof(VehicleContainerType))]
    public class UpdateContainerTypeDto
    {
        [Required]
        public long Id { get; set; }

        private string _containertype;
        private string _remark;

        [MaxLength(250)]
        public string container_type
        {
            get => _containertype;
            set => _containertype = value?.Trim();
        }

        [MaxLength(600)]
        public string remark
        {
            get => _remark;
            set => _remark = value?.Trim();
        }
    }
    [AutoMapTo(typeof(VehicleContainerType))]
    public class UpdateStatusContainerTypeDto
    {
        [Required]
        public long Id { get; set; }
    }

    [AutoMapFrom(typeof(VehicleContainerType))]
    public class ContainerTypeDto : EntityDto<long>
    {

        public string container_type { get; set; }

        public string remark { get; set; }
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
        public bool isactive { get; set; }
        public DateTime created_at { get; set; } 
        public DateTime? Updated_at { get; set; }

    }

    public class ContainerTypePagedRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
