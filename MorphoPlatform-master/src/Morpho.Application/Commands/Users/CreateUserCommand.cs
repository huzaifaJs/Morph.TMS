using Morpho.Application.Commands;
using Morpho.Domain.ValueObjects;

namespace Morpho.Application.Commands.Users
{
    /// <summary>
    /// Command to create a new user.
    /// </summary>
    public class CreateUserCommand : Command<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public Email EmailAddress { get; set; }
        public string Password { get; set; }
        public int? TenantId { get; set; }
        public bool IsActive { get; set; } = true;
        public string[] RoleNames { get; set; } = [];
    }
}