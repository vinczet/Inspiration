using ErrorOr;

namespace Inspiration.Api.Common;

public static class GetSignedInUser
{
    /// <summary>
    /// Gets the userId from the header claims (Jwt)
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static ErrorOr<Guid> GetUserId(HttpContext httpContext)
    {        
        var userClaim = httpContext?.User?.Claims.FirstOrDefault();
        if (userClaim == null)
        {
            return new List<Error> { Domain.Errors.Authentication.InvalidCredentials };
        }
        var userId = userClaim.Value;
        Guid userIdGuid;
        if (!Guid.TryParse(userId, out userIdGuid))
        {
            return new List<Error> { Domain.Errors.Authentication.InvalidCredentials };
        }
        return userIdGuid;
    }
}
