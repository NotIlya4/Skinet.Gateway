namespace Infrastructure.AccountService;

public record JwtTokenPair
{
    public string JwtToken { get; }
    public Guid RefreshToken { get; }

    public JwtTokenPair(string jwtToken, string refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = new Guid(refreshToken);
    }
}