namespace Infrastructure.AccountService;

public class AccountServiceUrlProvider
{
    public string Base { get; }
    public string Login { get; }
    public string Register { get; }
    public string UpdateJwtPair { get; }

    public string GetUserById(string userId)
    {
        return $"{Base}id/{userId}";
    }

    public AccountServiceUrlProvider(string @base, string login, string register, string updateJwtPair)
    {
        Base = @base;
        Login = login;
        Register = register;
        UpdateJwtPair = updateJwtPair;
    }
}