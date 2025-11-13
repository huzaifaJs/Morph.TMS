using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;

namespace Morpho.Users.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        [DisableAuditing]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength, MinimumLength = 6)]
        [DisableAuditing]
        public string NewPassword { get; set; }
    }
}
