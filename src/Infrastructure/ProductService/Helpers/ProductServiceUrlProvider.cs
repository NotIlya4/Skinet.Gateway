namespace Infrastructure.ProductService;

public class ProductServiceUrlProvider
{
    public Uri Brands { get; }
    public Uri ProductTypes { get; }
    public Uri Products { get; }
    private readonly Uri _baseUrl;
    private readonly string _getProductByIdPath;

    public Uri MakeGetProductByIdUrl(Guid productId)
    {
        return new Uri(_baseUrl, _getProductByIdPath.Replace("{id}", productId.ToString()));
    }

    public ProductServiceUrlProvider(string baseUrl, string brandsPath, string productTypesPath, string productsPath, string getProductByIdPath)
    {
        var uriBuilder = new UriBuilder(baseUrl);
        _baseUrl = uriBuilder.Uri;

        uriBuilder.Path = brandsPath;
        Brands = uriBuilder.Uri;
        
        uriBuilder.Path = productTypesPath;
        ProductTypes = uriBuilder.Uri;
        
        uriBuilder.Path = productsPath;
        Products = uriBuilder.Uri;

        _getProductByIdPath = getProductByIdPath;
    }
}