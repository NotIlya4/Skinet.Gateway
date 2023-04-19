namespace Infrastructure.AccountService;

public record UserInfo
{
    public string Email { get; }
    public Address? Address { get; }

    public UserInfo(string email, Address? address)
    {
        Email = email;
        Address = address;
    }
}