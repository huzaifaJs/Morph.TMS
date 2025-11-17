using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.Devices
{
    [Table("tracking_devices")]
    public class TrackingDevices : Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public int TenantId { get; set; }
        [ForeignKey("device_type")]
        public long device_type_id { get; set; }
        public string device_unique_no { get; set; }
        public string device_type { get; set; }
        public string device_name { get; set; }
        public string manufacturer { get; set; }
        public string model_no { get; set; }
        public string remark { get; set; }
        public string serial_number { get; set; }
        public string imei_number { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal min_value { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? max_value { get; set; }
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
