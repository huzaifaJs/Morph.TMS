using Abp.Application.Services;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.VehicleDocsType
{
    public interface IVehicleDocsTypeAppService : IApplicationService
    {
        Task<List<VechicleDocsTypeDto>> GetVehicleDocsTypeListAsync();
        Task<CreateVechicleDocsTypeDto> AddVehiclDocsTypeAsync(CreateVechicleDocsTypeDto input);
        Task<UpdateVechicleDocsTypeDto> UpdateVehicleDocsTypeAsync(UpdateVechicleDocsTypeDto input);
        Task<UpdateStatusVechicleDocsTypeDto> DeleteVehicleDocsTypeAsync(UpdateStatusVechicleDocsTypeDto input);
        Task<UpdateStatusVechicleDocsTypeDto> UpdateVehicleDocsTypeStatusAsync(UpdateStatusVechicleDocsTypeDto input);
        Task<VechicleDocsTypeDto> GetVehicleDocsTypeDetailsAsync(long vehicleDocstypeId);
        Task<List<VechicleDocsTypeDto>> GetVehicleDDLDocsTypeListAsync();
    }
}
