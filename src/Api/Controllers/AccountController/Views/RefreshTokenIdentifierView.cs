namespace Api.Controllers.AccountController.Views;

public class RefreshTokenIdentifierView
{
    public required string UserId { get; init; }
    public required string RefreshToken { get; init; }
}