namespace Inspiration.Contract.Authentication;

public record RegisterRequest(string Name, string Email, string Password);