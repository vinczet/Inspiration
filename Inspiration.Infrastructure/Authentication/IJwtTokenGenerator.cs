namespace Inspiration.Infrastructure.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string user);
}
