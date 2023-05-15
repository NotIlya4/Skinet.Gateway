using Api.Swagger.Enrichers.ProductController;

namespace Api.Controllers.ProductController.View;

public class ProductView
{
    public Guid Id { get; }
    [ProductName]
    public string Name { get; }
    [ProductDescription]
    public string Description { get; }
    public Uri PictureUrl { get; }
    [ProductPrice]
    public decimal Price { get; }
    [ProductBrand]
    public string Brand { get; }
    [ProductType]
    public string ProductType { get; }

    public ProductView(Guid id, string name, string description, Uri pictureUrl, decimal price, string brand, string productType)
    {
        Id = id;
        Name = name;
        Description = description;
        PictureUrl = pictureUrl;
        Price = price;
        Brand = brand;
        ProductType = productType;
    }
}