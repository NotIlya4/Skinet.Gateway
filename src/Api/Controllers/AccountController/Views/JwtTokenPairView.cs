using Api.Swagger.Enrichers.AccountController;

namespace Api.Controllers.AccountController.Views;

public class JwtTokenPairView
{
    [JwtToken]
    public string JwtToken { get; }
    public Guid RefreshToken { get; }

    public JwtTokenPairView(string jwtToken, Guid refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }
}