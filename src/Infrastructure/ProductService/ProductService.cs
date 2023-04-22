using Infrastructure.Client;
using Infrastructure.ProductService.Helpers;
using Infrastructure.ProductService.Models;
using Newtonsoft.Json.Linq;

namespace Infrastructure.ProductService;

public class ProductService : IProductService
{
    private readonly IServiceHttpClient _client;
    private readonly ProductServiceUrlProvider _urlProvider;

    public ProductService(IServiceHttpClient client, ProductServiceUrlProvider urlProvider)
    {
        _client = client;
        _urlProvider = urlProvider;
    }
    
    public async Task<List<string>> GetBrands()
    {
        string body = await _client.GetRaw(_urlProvider.Brands);
        return ParseStringList(body);
    }

    public async Task<List<string>> GetProductTypes()
    {
        string body = await _client.GetRaw(_urlProvider.ProductTypes);
        return ParseStringList(body);
    }

    public async Task<GetProductsResponse> GetProducts(ProductFilteringAndSorting productFilteringAndSorting)
    {
        return await _client.Get<GetProductsResponse>(_urlProvider.Products, productFilteringAndSorting);
    }

    public async Task<Product> GetProduct(Guid productId)
    {
        return await _client.Get<Product>(_urlProvider.MakeGetProductByIdUrl(productId));
    }

    private List<string> ParseStringList(string body)
    {
        return JArray.Parse(body).ToObject<List<string>>() ?? throw new InvalidOperationException("Response has invalid format");
    }
}