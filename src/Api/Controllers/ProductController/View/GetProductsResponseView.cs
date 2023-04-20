namespace Api.Controllers.ProductController.View;

public class GetProductsResponseView
{
    public List<ProductView> Products { get; }
    public int Total { get; }

    public GetProductsResponseView(List<ProductView> products, int total)
    {
        Products = products;
        this.Total = total;
    }
}