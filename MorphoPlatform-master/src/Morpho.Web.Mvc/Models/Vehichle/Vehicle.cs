using Morpho.Roles.Dto;
using Morpho.Users.Dto;
using Morpho.VehicleDocs.DocsVehicle.Dto;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType.Dto;
using System.Collections.Generic;

namespace Morpho.Web.Models.Vehichle
{
    public class VehicleViewModel
    {
        public CreateVehicleDto CreateVehicleDto { get; set; }

        public UpdateVehicleDto UpdateVehicleDto { get; set; }

        // For Listing (optional)
        public IReadOnlyList<VehicleDto> VehicleDto { get; set; }

        public VehicleViewModel()
        {
            CreateVehicleDto = new CreateVehicleDto();
            UpdateVehicleDto = new UpdateVehicleDto();
            VehicleDto = new List<VehicleDto>();
        }

        public IReadOnlyList<VehicleDto> LstVehicles { get; set; }
            = new List<VehicleDto>();
    }
    public class VehicleDocsViewModel
    {
        public CreateDocsVehicleDto CreateDocsVehicleDto { get; set; }

        public UpdateDocsVehicleDto UpdateDocsVehicleDto { get; set; }

        public IReadOnlyList<DocsVehicleDto> DocsVehicleDto { get; set; }

        public VehicleDocsViewModel()
        {
            CreateDocsVehicleDto = new CreateDocsVehicleDto();
            UpdateDocsVehicleDto = new UpdateDocsVehicleDto();
            DocsVehicleDto = new List<DocsVehicleDto>();
        }

        public IReadOnlyList<DocsVehicleDto> LstVehicles { get; set; }
            = new List<DocsVehicleDto>();
    }
}