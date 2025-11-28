using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.VehicleDocument
{
    [Table("vehicle_documents")]
    public class VehicleDocument : Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public int TenantId { get; set; }

        public long document_type_id { get; set; }

        public long vehicle_id { get; set; }
        [ForeignKey(nameof(vehicle_id))]
        public Domain.Entities.Vehicles.Vehicles mainVehicles { get; set; }
        [ForeignKey(nameof(document_type_id))]
        public Domain.Entities.VehicleDocumentType.VehicleDocumentType mainVehicleDocumentType { get; set; }
        public string document_number { get; set; }
        public string document_docs_url { get; set; }
        public DateTime? issue_date { get; set; }
        public DateTime? expiry_date { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; } = true;
        public DateTime? active_at { get; set; }
        public long? created_by { get; set; }
        public long? updated_by { get; set; }
        public long? deleted_by { get; set; }
        public long? statu_updated_by { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? Updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? status_updated__at { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
