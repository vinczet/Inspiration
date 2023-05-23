using ErrorOr;
using Inspiration.Contract.Authentication;
using Inspiration.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth = Inspiration.Domain.Services.Interfaces;

namespace Inspiration.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly Auth.IAuthenticationService _authenticationService;

    public AuthenticationController(Auth.IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(request.Name, request.Email, request.Password);

        return authResult.Match(
            authResultValue => Ok(authResultValue.ConvertToAuthenticationResponse()),
            errors => Problem(errors));
    }


    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);

        return authResult.Match(
            authResultValue => Ok(authResultValue.ConvertToAuthenticationResponse()),
            errors => Problem(errors));
    }
}
