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

namespace Morpho.ShipmentPackage
{
    public interface IPackageTypeAppService : IApplicationService
    {
        Task<List<PackageTypeDto>> GetPackageTypeListAsync();
        Task<CreatePackageTypeDto> AddPackageTypeAsync(CreatePackageTypeDto input);
        Task<UpdatePackageTypeDto> UpdatePackageTypeAsync(UpdatePackageTypeDto input);
        Task<UpdateStatusPackageTypeDto> DeletePackageTypeAsync(UpdateStatusPackageTypeDto input);
        Task<UpdateStatusPackageTypeDto> UpdatePackageTypeStatusAsync(UpdateStatusPackageTypeDto input);
        Task<PackageTypeDto> GetPackageTypeDetailsAsync(long packageTypeId);
    }
}
