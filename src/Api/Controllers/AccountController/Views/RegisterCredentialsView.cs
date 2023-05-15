using Api.Swagger.Enrichers.AccountController;

namespace Api.Controllers.AccountController.Views;

public class RegisterCredentialsView
{
    [Username]
    public string Username { get; }
    [Email]
    public string Email { get; }
    [Password]
    public string Password { get; }

    public RegisterCredentialsView(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}