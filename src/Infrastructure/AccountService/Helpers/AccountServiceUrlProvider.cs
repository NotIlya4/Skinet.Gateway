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
    private readonly string _isEmailBusyPath;
    private readonly string _isUsernameBusyPath;

    public Uri MakeGetUserByIdUrl(string userId)
    {
        return new Uri(_baseUrl, _getUserByIdPath.Replace("{id}", userId));
    }
    
    public Uri MakeGetUserByJwtTokenUrl(string jwtToken)
    {
        return new Uri(_baseUrl, _getUserByJwtPath.Replace("{jwt}", jwtToken));
    }

    public Uri MakeIsEmailBusyUrl(string email)
    {
        return new Uri(_baseUrl, _isEmailBusyPath.Replace("{email}", email));
    }

    public Uri MakeIsUsernameBusyUrl(string username)
    {
        return new Uri(_baseUrl, _isUsernameBusyPath.Replace("{username}", username));
    }

    public AccountServiceUrlProvider(string baseUrl, string loginPath, string logoutPath, string registerPath, 
        string updateJwtPairPath, string getUserByIdPath, string getUserByJwtPath, string isEmailBusyPath, string isUsernameBusyPath)
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
        _isEmailBusyPath = isEmailBusyPath;
        _isUsernameBusyPath = isUsernameBusyPath;
    }
}