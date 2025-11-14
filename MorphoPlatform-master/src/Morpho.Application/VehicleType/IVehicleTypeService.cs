using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleType
{
    public interface IVehicleTypeService
    {
        Task<CreateVehicleTypeDto> AddVehicleTypeAsync(CreateVehicleTypeDto input);
        Task<List<VehicleTypeDto>> GetVehicleTypesListAsync();
        Task<UpdateVehicleTypeDto> UpdateVehicleTypeAsync(UpdateVehicleTypeDto input);
        Task<UpdateStatusVehicleTypeDto> UpdateVehicleTypeStatusAsync(UpdateStatusVehicleTypeDto input);
        Task<UpdateStatusVehicleTypeDto> DeleteVehicleTypeAsync(UpdateStatusVehicleTypeDto input);
        Task<VehicleTypeDto> GetVehicleTypeDetailsAsync(int VehicleTypeId);
    }
}
