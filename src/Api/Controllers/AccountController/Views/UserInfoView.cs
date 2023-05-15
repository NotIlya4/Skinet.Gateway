using Api.Swagger.Enrichers.AccountController;

namespace Api.Controllers.AccountController.Views;

public class UserInfoView
{
    public Guid Id { get; }
    [Username]
    public string Username { get; }
    [Email]
    public string Email { get; }

    public UserInfoView(Guid id, string username, string email)
    {
        Id = id;
        Username = username;
        Email = email;
    }
}