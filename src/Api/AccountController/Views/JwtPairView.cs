namespace Api.AccountController.Views;

public class JwtPairView
{
    public required string JwtToken { get; init; }
    public required string RefreshToken { get; init; }
}