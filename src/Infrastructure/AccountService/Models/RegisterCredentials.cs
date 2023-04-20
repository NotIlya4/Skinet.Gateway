namespace Infrastructure.AccountService.Models;

public record RegisterCredentials
{
    public string Email { get; }
    public string Password { get; }

    public RegisterCredentials(string email, string password)
    {
        Email = email;
        Password = password;
    }
}