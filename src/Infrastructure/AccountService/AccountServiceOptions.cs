namespace Infrastructure.AccountService;

public record AccountServiceOptions
{
    public string BaseUrl { get; }

    public AccountServiceOptions(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
}