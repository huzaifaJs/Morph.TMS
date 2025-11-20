using Abp.Application.Services;
using Morpho.ShipmentPackage.Package.Dto;
using Morpho.Vehicles.VehicleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.ShipmentPackage.Package
{
    public interface IPackageAppService : IApplicationService
    {
        Task<List<PackageDto>> GetPackageListAsync();
        Task<CreatePackageDto> AddPackageAsync(CreatePackageDto input);
        Task<UpdatePackageDto> UpdatePackageAsync(UpdatePackageDto input);
        Task<UpdateStatusPackageDto> DeletePackageAsync(UpdateStatusPackageDto input);
        Task<UpdateStatusPackageDto> UpdatePackageStatusAsync(UpdateStatusPackageDto input);
        Task<PackageDto> GetPackageDetailsAsync(long packageTypeId);
    }
}
