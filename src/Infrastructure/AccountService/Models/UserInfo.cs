namespace Infrastructure.AccountService.Models;

public record UserInfo
{
    public Guid Id { get; }
    public string Email { get; }
    public Address? Address { get; }

    public UserInfo(Guid id, string email, Address? address)
    {
        Id = id;
        Email = email;
        Address = address;
    }
}