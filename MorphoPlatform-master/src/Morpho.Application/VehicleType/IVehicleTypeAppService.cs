using Abp.Application.Services;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleType
{
    public interface IVehicleTypeAppService: IApplicationService
    {
        Task<List<VehicleTypeDto>> GetVehicleTypesListAsync();
        Task<CreateVehicleTypeDto> AddVehicleTypeAsync(CreateVehicleTypeDto input);
        Task<UpdateVehicleTypeDto> UpdateVehicleTypeAsync(UpdateVehicleTypeDto input);
        Task<UpdateStatusVehicleTypeDto> UpdateVehicleTypeStatusAsync(UpdateStatusVehicleTypeDto input);
        Task<VehicleTypeDto> GetVehicleTypeDetailsAsync(long vehicleTypeId);
    }
}
