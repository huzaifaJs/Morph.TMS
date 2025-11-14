using Morpho.Roles.Dto;
using Morpho.Users.Dto;
using Morpho.VehicleType.Dto;
using System.Collections.Generic;

namespace Morpho.Web.Models.Vehichle
{
    public class VehicleType
    {
        public CreateVehicleTypeDto CreateVehicleType { get; set; }

    }
    public class VehicleTypeViewModel
    {
        public IReadOnlyList<VehicleTypeDto> VehicleTypes { get; set; }
    }
}
