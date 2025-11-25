using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.Domain.Entities.Vehicles
{
    [Table("vehicle")]
    public class Vehicles : Entity<long>, IMustHaveTenant, ISoftDelete
    {
        public int TenantId { get; set; }
        public string vehicle_unqiue_id { get; set; }
        [ForeignKey(nameof(VehicleType))]
        public long? vehicle_types_id { get; set; }
        public VehicleTypes VehicleType { get; set; }
        [ForeignKey(nameof(FuelType))]
        public long? fuel_types_id { get; set; }
        public Domain.Entities.FuelType.FuelType FuelType { get; set; }
        public string vehicle_number { get; set; }
        public string vehicle_name  { get; set; }
        public string model_name { get; set; }
        public string manufacturer { get; set; }
        public string remark { get; set; }
        public int manufacturing_year { get; set; }
        public string chassis_number { get; set; }
        public string engine_number { get; set; }
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
