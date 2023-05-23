using ErrorOr;
using Inspiration.Contract.Authentication;

namespace Inspiration.Domain.Services.Interfaces;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Register(string name, string email, string password);
    ErrorOr<AuthenticationResult> Login(string email, string password);
}
