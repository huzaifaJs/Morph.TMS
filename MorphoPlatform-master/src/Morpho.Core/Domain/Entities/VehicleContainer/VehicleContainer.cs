using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.VehicleContainer
{

    [Table("vehicle_containers")]
    public class VehicleContainer : Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public string container_number { get; set; }
        [ForeignKey(nameof(VehicleDocumentType))]
        public long container_type_id { get; set; }
        public VehicleDocumentType.VehicleDocumentType VehicleDocumentType { get; set; }
        public int TenantId { get; set; }
        public string container_unqiue_id { get; set; }
        public string weight_capacity { get; set; }
        public string ownership { get; set; }
        public string load_status { get; set; } = "Empty";
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
        public long? deleted_by { get; set; }
        public long? block_by { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? Updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? block_at { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool isblock { get; set; } = false;
    }
}
