namespace Api.Controllers.AccountController.Views;

public class UserInfoView
{
    public string Id { get; }
    public string Email { get; }
    public AddressView? Address { get; }

    public UserInfoView(string id, string email, AddressView? address)
    {
        Id = id;
        Email = email;
        Address = address;
    }
}