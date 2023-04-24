namespace Infrastructure.AccountService.Models;

public class JwtTokenPair
{
    public string JwtToken { get; }
    public Guid RefreshToken { get; }

    public JwtTokenPair(string jwtToken, string refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = new Guid(refreshToken);
    }
}