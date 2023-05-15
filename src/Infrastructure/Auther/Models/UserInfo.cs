namespace Infrastructure.Auther.Models;

public class UserInfo
{
    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }

    public UserInfo(Guid id, string username, string email)
    {
        Id = id;
        Username = username;
        Email = email;
    }
}