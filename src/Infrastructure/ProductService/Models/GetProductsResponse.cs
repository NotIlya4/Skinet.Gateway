namespace Infrastructure.ProductService;

public class GetProductsResponse
{
    public List<Product> Products { get; }
    public int Total { get; }

    public GetProductsResponse(List<Product> products, int total)
    {
        Products = products;
        this.Total = total;
    }
}