using Abp.Application.Services;
using Morpho.VehicleContainer.Container.Dto;
using Morpho.VehicleContainer.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleContainer
{
    public interface IVehicleContainerAppService : IApplicationService
    {
        Task<List<ContainerDto>> GetVehicleContainerListAsync();
        Task<CreateContainerDto> AddVehicleContainerAsync(CreateContainerDto input);
        Task<UpdateContainerDto> UpdateVehicleContainerAsync(UpdateContainerDto input);
        Task<UpdateStatusContainerDto> DeleteVehicleContainerAsync(UpdateStatusContainerDto input);
        Task<UpdateStatusContainerDto> UpdateVehicleContainerStatusAsync(UpdateStatusContainerDto input);
        Task<ContainerDto> GetVehicleContainerDetailsAsync(long vehicleContainerId);
    }
}
