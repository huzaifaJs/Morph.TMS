using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Morpho.Application.Queries.Users;
using Morpho.Authorization.Users;
using Morpho.Domain.Common;
using Morpho.Queries.Users;
using Morpho.Users.Dto;

namespace Morpho.Application.Queries.Users
{
    /// <summary>
    /// Handler for GetUserByIdQuery.
    /// </summary>
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IObjectMapper _objectMapper;

        public GetUserByIdQueryHandler(
            IRepository<User, long> userRepository,
            IObjectMapper objectMapper)
        {
            _userRepository = userRepository;
            _objectMapper = objectMapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetAsync(request.UserId);
                if (user == null)
                {
                    return Result.Failure<UserDto>($"User with ID {request.UserId} not found");
                }

                var userDto = _objectMapper.Map<UserDto>(user);
                return Result.Success(userDto);
            }
            catch (System.Exception ex)
            {
                return Result.Failure<UserDto>($"An error occurred while retrieving user: {ex.Message}");
            }
        }
    }
}