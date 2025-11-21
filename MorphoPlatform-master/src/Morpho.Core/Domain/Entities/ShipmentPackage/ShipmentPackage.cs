using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.ShipmentPackage
{
    [Table("shipment_package")]
    public class ShipmentPackage : Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public int TenantId { get; set; }
        public string package_number { get; set; }
        public string package_unique_id { get; set; }
        [ForeignKey("package_type")]
        public long? package_type_id { get; set; }

        [Column("weight_kg", TypeName = "decimal(18,2)")]
        public decimal? weight_kg { get; set; }

        [Column("volume_cbm", TypeName = "decimal(18,2)")]
        public decimal? volume_cbm { get; set; }
        public string dimension { get; set; }
        public string remarks { get; set; }
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
        public long? deleted_by { get; set; }
        public long? active_by { get; set; }
        public string status { get; set; } = "Pending";
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? Updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? active_at { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool isactive { get; set; } = true;

    }
}
