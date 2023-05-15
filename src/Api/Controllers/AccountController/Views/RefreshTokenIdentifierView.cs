namespace Api.Controllers.AccountController.Views;

public class RefreshTokenIdentifierView
{
    public Guid UserId { get; }
    public Guid RefreshToken { get; }

    public RefreshTokenIdentifierView(Guid userId, Guid refreshToken)
    {
        UserId = userId;
        RefreshToken = refreshToken;
    }
}