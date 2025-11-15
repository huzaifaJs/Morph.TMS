using Morpho.Roles.Dto;
using Morpho.Users.Dto;
using Morpho.VehicleType.Dto;
using System.Collections.Generic;

namespace Morpho.Web.Models.Vehichle
{
    public class VehicleTypeViewModel
    {
        public CreateVehicleTypeDto CreateVehicleType { get; set; }

        public UpdateVehicleTypeDto UpdateVehicleType { get; set; }

        // For Listing (optional)
        public IReadOnlyList<VehicleTypeDto> VehicleTypes { get; set; }

        public VehicleTypeViewModel()
        {
            CreateVehicleType = new CreateVehicleTypeDto();
            UpdateVehicleType = new UpdateVehicleTypeDto();
            VehicleTypes = new List<VehicleTypeDto>();
        }

        public IReadOnlyList<VehicleTypeDto> LstVehicleTypes { get; set; }
            = new List<VehicleTypeDto>();
    }

}