using Morpho.Application.Queries;
using Morpho.Users.Dto;

namespace Morpho.Queries.Users
{
    /// <summary>
    /// Query to get a user by ID.
    /// </summary>
    public class GetUserByIdQuery : Query<UserDto>
    {
        public long UserId { get; set; }

        public GetUserByIdQuery(long userId)
        {
            UserId = userId;
        }
    }
}