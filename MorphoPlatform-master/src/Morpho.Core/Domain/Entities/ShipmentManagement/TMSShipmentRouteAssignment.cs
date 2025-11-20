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
    [Table("tms_shipment_route_assignment")]
    public class TMSShipmentRouteAssignment : Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public int TenantId { get; set; }
        public long? shipment_id { get; set; }
        public long? vehicle_container_id { get; set; }
        public long? vehicle_id { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public long? assigned_by { get; set; }
        public DateTime AssignedOn { get; set; } = DateTime.UtcNow;
        public bool is_active { get; set; } = true;
        public string remarks { get; set; }
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
        public long? deleted_by { get; set; }
        public long? active_by { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? Updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? active_at { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool isactive { get; set; } = true;
    }
}
