using Api.Controllers.ProductController.View;
using Infrastructure.ProductService;

namespace Api.Controllers.ProductController;

public class ProductControllerViewMapper
{
    public ProductFilteringAndSorting MapProductFilteringAndSorting(ProductFilteringAndSortingView view)
    {
        return new ProductFilteringAndSorting(
            offset: view.Offset,
            limit: view.Limit,
            productType: view.ProductType,
            brand: view.Brand,
            searching: view.Searching,
            sortings: view.Sortings);
    }

    public GetProductsResponseView MapGetProductsResponse(GetProductsResponse getProductsResponse)
    {
        return new GetProductsResponseView(
            products: getProductsResponse.Products.Select(MapProduct).ToList(),
            total: getProductsResponse.Total);
    }

    public ProductView MapProduct(Product product)
    {
        return new ProductView(
            id: product.Id,
            name: product.Name,
            description: product.Description,
            pictureUrl: product.PictureUrl,
            price: product.Price,
            brand: product.Brand,
            productType: product.ProductType);
    }
}