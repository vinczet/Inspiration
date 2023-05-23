namespace Inspiration.Contract.Authentication;

public record AuthenticationResult(UserDto User, string Token);