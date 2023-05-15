using Api.Swagger.Enrichers.AccountController;

namespace Api.Controllers.AccountController.Views;

public class LoginCredentialsView
{
    [Email]
    public string Email { get; }
    [Password]
    public string Password { get; }

    public LoginCredentialsView(string email, string password)
    {
        Email = email;
        Password = password;
    }
}