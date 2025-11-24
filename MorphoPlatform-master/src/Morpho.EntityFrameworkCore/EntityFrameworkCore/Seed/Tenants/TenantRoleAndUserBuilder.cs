using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Morpho.Authorization;
using Morpho.Authorization.Roles;
using Morpho.Authorization.Users;

namespace Morpho.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly MorphoDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(MorphoDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // =============== TENANT ADMIN ROLE ===============

            var adminRole = _context.Roles
                .IgnoreQueryFilters()
                .FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);

            if (adminRole == null)
            {
                adminRole = new Role(
                    _tenantId,
                    StaticRoleNames.Tenants.Admin,
                    StaticRoleNames.Tenants.Admin
                )
                {
                    IsStatic = true
                };

                _context.Roles.Add(adminRole);
                _context.SaveChanges();
            }

            // =============== PERMISSIONS FOR ADMIN ROLE ===============

            var grantedPermissions = _context.Permissions
                .IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == adminRole.Id)
                .Select(p => p.Name)
                .ToList();

            var allPermissions = PermissionFinder
                .GetAllPermissions(new MorphoAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                .ToList();

            var permissionsToAdd = allPermissions
                .Where(p => !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissionsToAdd.Any())
            {
                foreach (var permission in permissionsToAdd)
                {
                    _context.Permissions.Add(new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    });
                }

                _context.SaveChanges();
            }

            // =============== TENANT ADMIN USER ===============

            var adminUser = _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);

            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");

                adminUser.Password = new PasswordHasher<User>(
                    new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions()))
                    .HashPassword(adminUser, "123qwe");

                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();
            }

            // =============== ASSIGN ROLE TO USER (SAFE) ===============

            bool roleAlreadyAssigned = _context.UserRoles
                .IgnoreQueryFilters()
                .Any(ur => ur.TenantId == _tenantId
                        && ur.UserId == adminUser.Id
                        && ur.RoleId == adminRole.Id);

            if (!roleAlreadyAssigned)
            {
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }
        }
    }
}
