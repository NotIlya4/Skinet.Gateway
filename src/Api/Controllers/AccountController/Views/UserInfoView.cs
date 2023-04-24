namespace Api.Controllers.AccountController.Views;

public class UserInfoView
{
    public string Id { get; }
    public string Username { get; }
    public string Email { get; }
    public AddressView? Address { get; }

    public UserInfoView(string id, string username, string email, AddressView? address)
    {
        Id = id;
        Username = username;
        Email = email;
        Address = address;
    }
}