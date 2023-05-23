using Inspiration.Contract;
using Inspiration.Contract.Authentication;
using Inspiration.Repository.DataAccessObjects;

namespace Inspiration.Domain;

public static class Converter
{
    public static UserResponseDto ConvertToUserResponseDto(this UserDao user)
    {
        return new UserResponseDto(user.Id, user.Name);
    }

    public static UserDto ConvertToUserDto(this UserDao user)
    {
        return new UserDto()
        { 
            Id = user.Id,
            Name = user.Name,
            Password= user.Password,
            Email= user.Email,
            IsAdmin = user.IsAdmin
        };
    }

    public static AuthenticationResponse ConvertToAuthenticationResponse(this AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.Name,
            authResult.User.Email,
            authResult.User.IsAdmin,
            authResult.Token);
    }

    public static NPUResponseDto ConvertToNPUResponseDto(this NPUListItemDao dao, string url)
    {
        return new NPUResponseDto(
            dao.Id,
            new UserResponseDto(dao.UserId, dao.UserName),
            dao.Description,
            url,
            dao.IsEnabled,
            dao.CreationDateTime,
            dao.UpdateDateTime,
            dao.Parts,
            dao.AverageCreativityRating,
            dao.CreativityRatings,
            dao.AverageUniquenessRating,
            dao.UniquenessRatings);
    }
}
