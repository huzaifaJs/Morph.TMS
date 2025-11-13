using System.Collections.Generic;
using Morpho.Roles.Dto;

namespace Morpho.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}