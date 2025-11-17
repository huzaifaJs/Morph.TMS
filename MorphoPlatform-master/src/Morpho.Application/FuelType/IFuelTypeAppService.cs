using Abp.Application.Services;
using Morpho.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho.FuelType
{
    public interface IFuelTypeAppService : IApplicationService
    {
        Task<List<FuelTypeDto>> GetFuelTypesListAsync();
        Task<CreateFuelTypeDto> AddFuelTypeAsync(CreateFuelTypeDto input);
        Task<UpdateFuelTypeDto> UpdateFuelTypeAsync(UpdateFuelTypeDto input);
        Task<UpdateStatusFuelTypeDto> DeleteFuelTypeAsync(UpdateStatusFuelTypeDto input);
        Task<UpdateStatusFuelTypeDto> UpdateFuelTypeStatusAsync(UpdateStatusFuelTypeDto input);
        Task<FuelTypeDto> GetFuelTypeDetailsAsync(long fuelTypeId);
    }
}
