using Api.Swagger.Enrichers.ProductController;

namespace Api.Controllers.ProductController.View;

public class GetProductsResponseView
{
    public List<ProductView> Products { get; }
    [ProductsTotal]
    public int Total { get; }

    public GetProductsResponseView(List<ProductView> products, int total)
    {
        Products = products;
        this.Total = total;
    }
}