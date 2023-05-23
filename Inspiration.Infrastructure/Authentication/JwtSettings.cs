namespace Inspiration.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SECTIONNAME = "JWTSettings";
    public string Secret { get; init; } = "";
    public string Issuer { get; init; } = "";
    public string Audience { get; init; } = "";
    public int ExpiryHours { get; init; }
}
