using System.Collections.Generic;
using Morpho.Roles.Dto;

namespace Morpho.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
