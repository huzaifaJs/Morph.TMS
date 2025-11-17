using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities
{
    [Table("device_type")]
    public class DeviceType : Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public int TenantId { get; set; }
        public string device_type_name { get; set; }
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
        public bool IsDeleted { get; set; } = false; 
    }
}
