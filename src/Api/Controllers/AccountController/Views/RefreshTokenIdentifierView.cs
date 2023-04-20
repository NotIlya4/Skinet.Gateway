namespace Api.Controllers.AccountController.Views;

public class RefreshTokenIdentifierView
{
    public string UserId { get; }
    public string RefreshToken { get; }

    public RefreshTokenIdentifierView(string userId, string refreshToken)
    {
        UserId = userId;
        RefreshToken = refreshToken;
    }
}