using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.ShipmentManagement
{
    [Table("tms_shipment_status_log")]
    public class TMSShipmentStatusLog : Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public int TenantId { get; set; }
       [ForeignKey("tms_shipment")]
        public long shipment_id { get; set; }
        public string shipment_status { get; set; }
        public string remarks { get; set; }
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
        public long? deleted_by { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? Updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? active_at { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
