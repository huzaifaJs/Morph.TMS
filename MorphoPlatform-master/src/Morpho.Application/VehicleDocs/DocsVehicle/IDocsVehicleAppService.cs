using Abp.Application.Services;
using Morpho.VehicleDocs.DocsVehicle.Dto;
using Morpho.VehicleDocs.VechicleDocsType.Dto;
using Morpho.VehicleType.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.DocsVehicle
{
    public interface IDocsVehicleAppService : IApplicationService
    {
        Task<List<DocsVehicleDto>> GetDocsVehicleListAsync();
        Task<CreateDocsVehicleDto> AddDocsVehicleAsync(CreateDocsVehicleDto input);
        Task<UpdateDocsVehicleDto> UpdateDocsVehicleAsync(UpdateDocsVehicleDto input);
        Task<UpdateStatusDocsVehicleDto> DeleteDocsVehicleAsync(UpdateStatusDocsVehicleDto input);
        Task<UpdateStatusDocsVehicleDto> UpdateDocsVehicleStatusAsync(UpdateStatusDocsVehicleDto input);
        Task<DocsVehicleDto> GetDocsVehicleDetailsAsync(long vehicleDocstypeId);
    }
}
