using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Morpho.Domain.Entities.ShipmentManagement
{
    [Table("tms_shipment_package_assignment")]
    public class TMSShipmentpackageAssignment:Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public int TenantId { get; set; }
        [ForeignKey("vehicle_containers")]
        public long? vehicle_container_id { get; set; }
        [ForeignKey("tms_shipment")]
        public long? shipment_id { get; set; }
        [ForeignKey("shipment_package")]
        public long? package_id { get; set; }
        public long? assigned_by { get; set; }
        public DateTime? assigned_on { get; set; } 
        public bool is_active { get; set; } = true;
        public string remarks { get; set; }
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
        public long? deleted_by { get; set; }
        public long? active_by { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? active_at { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool isactive { get; set; } = true;
    }
}
