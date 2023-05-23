
using ErrorOr;
using Inspiration.Contract.Authentication;
using Inspiration.Domain.Services.Interfaces;
using Inspiration.Infrastructure.Authentication;
using Inspiration.Repository.DataAccessObjects;
using Inspiration.Repository.Interfaces;


namespace Inspiration.Domain.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        //Validate
        if (_userRepository.GetUserByEmail(email) is not UserDao user || user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        //Create JWT        
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Name);
        return new AuthenticationResult(user.ConvertToUserDto(), token);
    }

    public ErrorOr<AuthenticationResult> Register(string name, string email, string password)
    {
        #region Validations
        // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(email) != null)
        {
            return Errors.User.DuplicateEmail;
        }

        if (String.IsNullOrEmpty(name))
        {
            return Errors.User.EmptyName;
        }

        if (String.IsNullOrEmpty(email))
        {
            return Errors.User.EmptyEmail;
        }

        if (String.IsNullOrEmpty(password))
        {
            return Errors.User.EmptyPassword;
        }
        #endregion

        // 2. Create user (generate unique ID) & Persist
        var user = new UserDao(Guid.NewGuid(), name, email, password, false);
        _userRepository.Create(user);

        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Name);

        return new AuthenticationResult(user.ConvertToUserDto(), token);
    }

}
