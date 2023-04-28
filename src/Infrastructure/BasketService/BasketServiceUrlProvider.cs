namespace Infrastructure.BasketService;

public class BasketServiceUrlProvider
{
    public Uri PostBasket { get; }
    private readonly Uri _baseUrl;
    private readonly string _getBasketUrlPath;

    public Uri MakeGetBasketUrl(Guid userId)
    {
        return new Uri(_baseUrl, _getBasketUrlPath.Replace("{userId}", userId.ToString()));
    }

    public BasketServiceUrlProvider(string baseUrl, string postBasketPath, string getBasketUrlPath)
    {
        _baseUrl = new Uri(baseUrl);

        PostBasket = new Uri(_baseUrl, postBasketPath);

        _getBasketUrlPath = getBasketUrlPath;
    }
}