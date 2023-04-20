namespace Infrastructure.AccountService.Helpers;

public class AccountServiceUrlProvider
{
    public Uri Login { get; }
    public Uri Logout { get; }
    public Uri Register { get; }
    public Uri UpdateJwtPair { get; }
    private readonly Uri _baseUrl;
    private readonly string _getUserByIdPath;
    private readonly string _getUserByJwtPath;

    public Uri MakeGetUserByIdUrl(string userId)
    {
        return new Uri(_baseUrl, _getUserByIdPath.Replace("{id}", userId));
    }
    
    public Uri MakeGetUserByJwtTokenUrl(string jwtToken)
    {
        return new Uri(_baseUrl, _getUserByJwtPath.Replace("{jwt}", jwtToken));
    }

    public AccountServiceUrlProvider(string baseUrl, string loginPath, string logoutPath, string registerPath, string updateJwtPairPath, string getUserByIdPath, string getUserByJwtPath)
    {
        _baseUrl = new Uri(baseUrl);
        
        var uriBuilder = new UriBuilder(baseUrl);

        uriBuilder.Path = loginPath;
        Login = uriBuilder.Uri;

        uriBuilder.Path = logoutPath;
        Logout = uriBuilder.Uri;

        uriBuilder.Path = registerPath;
        Register = uriBuilder.Uri;

        uriBuilder.Path = updateJwtPairPath;
        UpdateJwtPair = uriBuilder.Uri;

        _getUserByIdPath = getUserByIdPath;
        _getUserByJwtPath = getUserByJwtPath;
    }
}