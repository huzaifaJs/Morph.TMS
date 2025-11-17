using Abp.Application.Services;
using Morpho.Vehicles.VehicleDto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleType
{
    public interface IVehicleAppService : IApplicationService
    {
        Task<List<VehicleDto>> GetVehicleListAsync();
        Task<CreateVehicleDto> AddVehicleAsync(CreateVehicleDto input);
        Task<UpdateVehicleDto> UpdateVehicleAsync(UpdateVehicleDto input);
        Task<UpdateStatusVehicleDto> DeleteVehicleAsync(UpdateStatusVehicleDto input);
        Task<UpdateStatusVehicleDto> UpdateVehicleStatusAsync(UpdateStatusVehicleDto input);
        Task<VehicleDto> GetVehicleDetailsAsync(long vehicleId);
    }
}
