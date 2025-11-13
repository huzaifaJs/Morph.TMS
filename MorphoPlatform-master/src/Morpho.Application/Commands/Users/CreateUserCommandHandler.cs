using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Morpho.Application.Commands.Users;
using Morpho.Authorization.Roles;
using Morpho.Authorization.Users;
using Morpho.Domain.Common;
using Morpho.Domain.Services;

namespace Morpho.Application.Commands.Users
{
    /// <summary>
    /// Handler for CreateUserCommand.
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<long>>
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly UserDomainService _userDomainService;
        private readonly IEventBus _eventBus;

        public CreateUserCommandHandler(
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            UserDomainService userDomainService,
            IEventBus eventBus)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _userDomainService = userDomainService;
            _eventBus = eventBus;
        }

        public async Task<Result<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Create user entity
                var user = new User
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    UserName = request.UserName,
                    EmailAddress = request.EmailAddress,
                    IsActive = request.IsActive,
                    TenantId = request.TenantId
                };

                user.SetNormalizedNames();

                // Create user
                var createResult = await _userManager.CreateAsync(user, request.Password);
                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    return Result.Failure<long>($"Failed to create user: {errors}");
                }

                // Assign roles if specified
                if (request.RoleNames?.Length > 0)
                {
                    var roleResult = await _userManager.SetRolesAsync(user, request.RoleNames);
                    if (!roleResult.Succeeded)
                    {
                        var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                        return Result.Failure<long>($"User created but failed to assign roles: {errors}");
                    }
                }

                // Publish domain event
                await _userDomainService.PublishUserRegisteredEventAsync(user);

                return Result.Success(user.Id);
            }
            catch (System.Exception ex)
            {
                return Result.Failure<long>($"An error occurred while creating user: {ex.Message}");
            }
        }
    }
}