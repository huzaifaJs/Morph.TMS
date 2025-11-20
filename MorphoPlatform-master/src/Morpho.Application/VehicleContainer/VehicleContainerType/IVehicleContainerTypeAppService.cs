using Abp.Application.Services;
using Morpho.VehicleContainer.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleContainer
{
    public interface IVehicleContainerTypeAppService : IApplicationService
    {
        Task<List<ContainerTypeDto>> GetVehicleContainerTypeListAsync();
        Task<CreateContainerTypeDto> AddVehicleContainerTypeAsync(CreateContainerTypeDto input);
        Task<UpdateContainerTypeDto> UpdateVehicleDocsTypeAsync(UpdateContainerTypeDto input);
        Task<UpdateStatusContainerTypeDto> DeleteVehicleContainerTypeAsync(UpdateStatusContainerTypeDto input);
        Task<UpdateStatusContainerTypeDto> UpdateVehicleContainerTypeStatusAsync(UpdateStatusContainerTypeDto input);
        Task<ContainerTypeDto> GetVehicleContainerTypeDetailsAsync(long vehicleDocstypeId);
    }
}
