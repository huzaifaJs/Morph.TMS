using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleDocsType;
using System.Collections.Generic;

namespace Morpho.Web.Models
{
    public class VehicleDocsTypeViewModel
    {
        public CreateVechicleDocsTypeDto CreateVehicleDocsType { get; set; }

        public UpdateVechicleDocsTypeDto UpdateVehicleDocsType { get; set; }

        // For Listing (optional)
        public IReadOnlyList<VechicleDocsTypeDto> VehicleTypeDocs { get; set; }

        public VehicleDocsTypeViewModel()
        {
            //CreateVehicleDocsType = new CreateVechicleDocsTypeDto();
            //UpdateVehicleDocsType = new UpdateVechicleDocsTypeDto();
           // VehicleTypeDocs = new List<VechicleDocsTypeDto>();
        }

        public IReadOnlyList<VechicleDocsTypeDto> LstVehicleTypes { get; set; }
            = new List<VechicleDocsTypeDto>();
    }
}
