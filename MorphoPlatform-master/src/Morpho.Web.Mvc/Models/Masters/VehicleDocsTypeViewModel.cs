using Morpho.Dto;
using Morpho.VehicleContainer.Dto;
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
            CreateVehicleDocsType = new CreateVechicleDocsTypeDto();
            UpdateVehicleDocsType = new UpdateVechicleDocsTypeDto();
            VehicleTypeDocs = new List<VechicleDocsTypeDto>();
        }

        public IReadOnlyList<VechicleDocsTypeDto> LstVehicleTypes { get; set; }
            = new List<VechicleDocsTypeDto>();
    }
    public class VehicleFuelTypeViewModel
    {
        public CreateFuelTypeDto CreateFuelTypeDto { get; set; }

        public UpdateFuelTypeDto UpdateFuelTypeDto { get; set; }

        // For Listing (optional)
        public IReadOnlyList<FuelTypeDto> FuelTypeDto { get; set; }

        public VehicleFuelTypeViewModel()
        {
            CreateFuelTypeDto = new CreateFuelTypeDto();
            UpdateFuelTypeDto = new UpdateFuelTypeDto();
            FuelTypeDto = new List<FuelTypeDto>();
        }

        public IReadOnlyList<VechicleDocsTypeDto> LstVehicleTypes { get; set; }
            = new List<VechicleDocsTypeDto>();
    }
    public class VehicleContainerTypeViewModel
    {
        public CreateContainerTypeDto CreateContainerTypeDto { get; set; }

        public UpdateContainerTypeDto UpdateContainerTypeDto { get; set; }

        // For Listing (optional)
        public IReadOnlyList<ContainerTypeDto> ContainerTypeDto { get; set; }

        public VehicleContainerTypeViewModel()
        {
            CreateContainerTypeDto = new CreateContainerTypeDto();
            UpdateContainerTypeDto = new UpdateContainerTypeDto();
            ContainerTypeDto = new List<ContainerTypeDto>();
        }

        public IReadOnlyList<ContainerTypeDto> LstContainerType { get; set; }
            = new List<ContainerTypeDto>();
    }
}
