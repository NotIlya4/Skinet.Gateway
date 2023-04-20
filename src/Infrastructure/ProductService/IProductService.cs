namespace Infrastructure.ProductService;

public interface IProductService
{
    public Task<List<string>> GetBrands();
    public Task<List<string>> GetProductTypes();
    public Task<GetProductsResponse> GetProducts(ProductFilteringAndSorting productFilteringAndSorting);
    public Task<Product> GetProduct(Guid productId);
}