namespace Infrastructure.Auther.Helpers;

public class AccountServiceUrlProvider
{
    private readonly Uri _baseUrl;
    private readonly string _getUserByJwtPath;
    
    public Uri MakeGetUserByJwtTokenUrl(string jwtToken)
    {
        return new Uri(_baseUrl, _getUserByJwtPath.Replace("{jwt}", jwtToken));
    }

    public AccountServiceUrlProvider(string baseUrl, string getUserByJwtPath)
    {
        _baseUrl = new Uri(baseUrl);
        _getUserByJwtPath = getUserByJwtPath;
    }
}