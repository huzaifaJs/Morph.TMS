using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities
{
    [Table("vehicle_types")]
    public class VehicleTypes: Entity<long>, IMustHaveTenant
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  long VehicleTypeId { get; set; }
        public Guid vehicle_type_id { get; set; } = Guid.NewGuid();
        public int TenantId { get; set; }
        [Required]
        [MaxLength(250)]
        public string vehicle_type_name { get; set; }
        [Required]
        [MaxLength(600)]
        public string remark { get; set; }
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
        public long? deleted_by { get; set; }
        public long? active_by { get; set; }
        public bool is_active { get; set; } = true;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? Updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? active_at { get; set; }
        public bool isdeleted { get; set; } = false;
    }
}
