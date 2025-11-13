using System.Collections.Generic;
using Morpho.Roles.Dto;

namespace Morpho.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
