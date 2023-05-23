namespace Inspiration.Contract.Authentication;

public record AuthenticationResponse(Guid Id, string Name, string Email, bool IsAdmin, string Token);