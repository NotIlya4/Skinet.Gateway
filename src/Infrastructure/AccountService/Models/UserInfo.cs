namespace Infrastructure.AccountService.Models;

public class UserInfo
{
    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }
    public Address? Address { get; }

    public UserInfo(Guid id, string username, string email, Address? address)
    {
        Id = id;
        Username = username;
        Email = email;
        Address = address;
    }
}